using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwp.ProjFinal.Models;
using Uwp.ProjFinal.Repositories.Base;

namespace Uwp.ProjFinal.Repositories
{
    public class EFAgendaItemRepository : Repository<AgendaItem>
    {
        private static readonly Lazy<EFAgendaItemRepository> _instance =
               new Lazy<EFAgendaItemRepository>(() => new EFAgendaItemRepository());

        public static EFAgendaItemRepository Instance { get { return _instance.Value; } }

        public override async Task LoadAll()
        {
            if (Items.Count > 0)
            {
                return;
            }

            using (var context = new AppDbContext())
            {
                var agendaItems = context.AgendaItens.ToList();

                foreach (var item in agendaItems)
                {
                    Items.Add(item);
                }
            }
        }

        public override async Task Create(AgendaItem entity)
        {
            var obj = new Object();
            using (var context = new AppDbContext())
            {
                await this.LoadAll();
                if (entity.Id == new Guid())
                    entity.Id = Guid.NewGuid();

                if (this.Items.Any(x => x.Id.Equals(entity.Id)))
                    await this.Update(entity);
                else
                {
                    context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.AgendaItens.AddAsync(entity);
                    await context.SaveChangesAsync();
                    Items.Add(entity);
                }
            }

            await StorageAgendaItemRepository.Instance.Create(entity);
        }

        public override async Task Update(AgendaItem entity)
        {
            using (var context = new AppDbContext())
            {
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await context.SaveChangesAsync();

                var collectionIndex = Items.Select((value, index) => new { value, index })
                    .Where(c => c.value.Id == entity.Id)
                    .Select(x => x.index)
                    .First();

                Items[collectionIndex] = entity;
            }

            await StorageAgendaItemRepository.Instance.Update(entity);
        }

        public override async Task Delete(AgendaItem entity)
        {
            var agendaItem = Items.SingleOrDefault(c => c.Id == entity.Id);

            if (agendaItem != null)
            {
                using (var context = new AppDbContext())
                {
                    Items.Remove(agendaItem);

                    context.AgendaItens.Remove(agendaItem);
                    await context.SaveChangesAsync();
                }
            }

            await StorageAgendaItemRepository.Instance.Delete(entity);
        }
    }
}
