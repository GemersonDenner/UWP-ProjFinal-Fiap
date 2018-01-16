using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwp.ProjFinal.Models;
using Uwp.ProjFinal.Repositories.Base;
using Uwp.ProjFinal.Services;

namespace Uwp.ProjFinal.Repositories
{

    public class StorageAgendaItemRepository : Repository<AgendaItem>
    {
        private static readonly Lazy<StorageAgendaItemRepository> _instance =
                new Lazy<StorageAgendaItemRepository>(() => new StorageAgendaItemRepository());

        public static StorageAgendaItemRepository Instance { get { return _instance.Value; } }

        public override async Task LoadAll()
        {
            if (Items.Count > 0)
            {
                return;
            }

            var agendaItem = await StorageService.LoadAllFiles<AgendaItem>(StorageService.Folders.AgendaItem);

            foreach (var agenda in agendaItem)
            {
                Items.Add(agenda);
            }
        }

        public override async Task Create(AgendaItem entity)
        {
            if (entity.Id == new Guid())
                entity.Id = Guid.NewGuid();

            if (Items.Any(x => x.Id.Equals(entity.Id)))
                await this.Update(entity);
            else
            {
                Items.Add(entity);

                await StorageService.SaveFile(entity, StorageService.Folders.AgendaItem, entity.Id.ToString());
            }
        }

        public override async Task Update(AgendaItem entity)
        {
            await StorageService.SaveFile(entity, StorageService.Folders.AgendaItem, entity.Id.ToString());
        }

        public override async Task Delete(AgendaItem entity)
        {
            var category = Items.SingleOrDefault(c => c.Id == entity.Id);

            if (category != null)
            {
                await StorageService.DeleteFile(StorageService.Folders.AgendaItem, category.Id.ToString());
                Items.Remove(category);
            }
        }
    }
}
