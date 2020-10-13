using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class ModelVisitorRemoveMarkedAndNewFlags : ModelVisitorBase
    {
        //protected override void BeginVisit(IGroupDocuments cn)
        //{
        //}
        protected override void BeginVisit(IGroupListCatalogs cn)
        {
            var p = (GroupListCatalogs)cn;
            if (p.IsHasMarkedForDeletion || p.IsHasNew)
            {
                foreach (var t in p.ListCatalogs)
                {
                    t.IsMarkedForDeletion = false;
                    t.IsNew = false;
                }
            }
        }
        protected override void BeginVisit(IGroupListMainViewForms cn)
        {
            var p = (GroupListMainViewForms)cn;
            if (p.IsHasMarkedForDeletion || p.IsHasNew)
            {
                foreach (var t in p.ListMainViewForms)
                {
                    t.IsMarkedForDeletion = false;
                    t.IsNew = false;
                }
            }
        }
        protected override void BeginVisit(IGroupListRoles cn)
        {
            var p = (GroupListRoles)cn;
            if (p.IsHasMarkedForDeletion || p.IsHasNew)
            {
                foreach (var t in p.ListRoles)
                {
                    t.IsMarkedForDeletion = false;
                    t.IsNew = false;
                }
            }
        }
        protected override void BeginVisit(IGroupListConstants cn)
        {
            var p = (GroupListConstants)cn;
            if (p.IsHasMarkedForDeletion || p.IsHasNew)
            {
                foreach (var t in p.ListConstants)
                {
                    t.IsMarkedForDeletion = false;
                    t.IsNew = false;
                }
            }
        }
        protected override void BeginVisit(IGroupListDocuments cn)
        {
            var p = (GroupListDocuments)cn;
            if (p.IsHasMarkedForDeletion || p.IsHasNew)
            {
                foreach (var t in p.ListDocuments)
                {
                    t.IsMarkedForDeletion = false;
                    t.IsNew = false;
                }
            }
        }
        protected override void BeginVisit(IGroupListEnumerations cn)
        {
            var p = (GroupListEnumerations)cn;
            if (p.IsHasMarkedForDeletion || p.IsHasNew)
            {
                foreach (var t in p.ListEnumerations)
                {
                    t.IsMarkedForDeletion = false;
                    t.IsNew = false;
                }
            }
        }
        protected override void BeginVisit(IGroupListForms cn)
        {
            var p = (GroupListForms)cn;
            if (p.IsHasMarkedForDeletion || p.IsHasNew)
            {
                foreach (var t in p.ListForms)
                {
                    t.IsMarkedForDeletion = false;
                    t.IsNew = false;
                }
            }
        }
        protected override void BeginVisit(IGroupListJournals cn)
        {
            var p = (GroupListJournals)cn;
            if (p.IsHasMarkedForDeletion || p.IsHasNew)
            {
                foreach (var t in p.ListJournals)
                {
                    t.IsMarkedForDeletion = false;
                    t.IsNew = false;
                }
            }
        }
        protected override void BeginVisit(IGroupListProperties cn)
        {
            var p = (GroupListProperties)cn;
            if (p.IsHasMarkedForDeletion || p.IsHasNew)
            {
                foreach (var t in p.ListProperties)
                {
                    t.IsMarkedForDeletion = false;
                    t.IsNew = false;
                }
            }
        }
        protected override void BeginVisit(IGroupListPropertiesTabs cn)
        {
            var p = (GroupListPropertiesTabs)cn;
            if (p.IsHasMarkedForDeletion || p.IsHasNew)
            {
                foreach (var t in p.ListPropertiesTabs)
                {
                    t.IsMarkedForDeletion = false;
                    t.IsNew = false;
                }
            }
        }
        protected override void BeginVisit(IGroupListReports cn)
        {
            var p = (GroupListReports)cn;
            if (p.IsHasMarkedForDeletion || p.IsHasNew)
            {
                foreach (var t in p.ListReports)
                {
                    t.IsMarkedForDeletion = false;
                    t.IsNew = false;
                }
            }
        }
    }
}
