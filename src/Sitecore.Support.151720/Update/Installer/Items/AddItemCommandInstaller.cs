using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Serialization.ObjectModel;
using Sitecore.Diagnostics;
using Sitecore.Update;
using Sitecore.Update.Commands;
using Sitecore.Update.Installer;
using Sitecore.Update.Installer.Items;
using Sitecore.Update.Installer.Utils;

namespace Sitecore.Support.Update.Installer.Items
{
  public class AddItemCommandInstaller : Sitecore.Update.Installer.Items.AddItemCommandInstaller
  {
    protected override Sitecore.Update.Installer.Items.AddItemCommandInstaller.ItemInstaller GetGeneralItemInstaller(AddItemCommand command, string commandKey)
    {
      Assert.ArgumentNotNull(command, "command");
      Assert.ArgumentNotNull(commandKey, "commandKey");
      return new ItemInstaller(commandKey, command, new LogMethod(this.Log));
    }

    protected class ItemInstaller : Sitecore.Update.Installer.Items.AddItemCommandInstaller.ItemInstaller
    {
      public ItemInstaller(string commandKey, AddItemCommand command, AddItemCommandInstaller.LogMethod loggerMethod)
        : base(commandKey, command, loggerMethod)
      {
      }

      protected override void UpdateSharedFields(string addCommandKey, Item sitecoreItem, SyncItem item, CommandInstallerContext context)
      {
        Assert.ArgumentNotNull(addCommandKey, "addCommandKey");
        Assert.ArgumentNotNull(item, "item");
        Assert.ArgumentNotNull(context, "context");
        if (sitecoreItem != null)
        {
          foreach (SyncField field in item.SharedFields)
          {
            ChangeEntry entry;
            ItemFieldChangedProcessor fieldProcessor = this.GetFieldProcessor();
            entry = new ChangeEntry("fieldvalue", string.Empty, field.FieldValue);
            Field field3 = sitecoreItem.Fields[field.FieldID];
            if (field3 != null)
            {
              fieldProcessor.Process(string.Concat(new object[] { addCommandKey, "_", fieldProcessor, "_", field.FieldID }), sitecoreItem, field3, entry, null, context);
            }
          }
        }
      }

    }
  }
}
