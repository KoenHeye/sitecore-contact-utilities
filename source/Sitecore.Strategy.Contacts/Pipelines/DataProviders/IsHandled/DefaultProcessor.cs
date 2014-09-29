﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Strategy.Contacts.DataProviders;
using Sitecore.Rules;

namespace Sitecore.Strategy.Contacts.Pipelines.DataProviders.IsHandled
{
    public class DefaultProcessor
    {
        protected static List<ID> AllHandledIds { get; private set; }
        protected static List<ID> FullyHandledIds { get; private set; }
        public DefaultProcessor()
        {
            if (FullyHandledIds == null)
            {
                FullyHandledIds = new List<ID>();
                FullyHandledIds.Add(Sitecore.Strategy.Contacts.DataProviders.ItemIDs.ContactsTemplatesFolder);

                FullyHandledIds.Add(Sitecore.Strategy.Contacts.DataProviders.ItemIDs.ContactsFolder);
                FullyHandledIds.Add(Sitecore.Strategy.Contacts.DataProviders.ItemIDs.ContactFacetsFolder);
            }
            if (AllHandledIds == null)
            {
                AllHandledIds = new List<ID>();
                AllHandledIds.AddRange(FullyHandledIds);
                AllHandledIds.Add(Sitecore.Strategy.Contacts.DataProviders.ItemIDs.SettingsRoot);
                AllHandledIds.Add(RuleIds.DefinitionsFolderId);
                AllHandledIds.Add(RuleIds.ElementsFolderID);
                AllHandledIds.Add(RuleIds.TagsFolderID);
                AllHandledIds.Add(RuleIds.MacrosesFolder);
            }
        }
        public virtual void Process(IsHandledArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNull(args.ItemId, "args.ItemId");
            List<ID> ids = null;
            if (args.IncludeAllIds)
            {
                ids = AllHandledIds;
            }
            else
            {
                ids = FullyHandledIds;
            }
            if (ids.Contains(args.ItemId))
            {
                args.IsHandled = true;
                return;
            }
            if (IDTableHelper.IsFacetItem(args.ItemId))
            {
                args.IsHandled = true;
                return;
            }
            if (IDTableHelper.IsFacetMemberItem(args.ItemId))
            {
                args.IsHandled = true;
                return;
            }
            if (IDTableHelper.IsFacetMemberValueItem(args.ItemId))
            {
                args.IsHandled = true;
                return;
            }
            //TODO: check dynamically generated item ids
        }
    }
}