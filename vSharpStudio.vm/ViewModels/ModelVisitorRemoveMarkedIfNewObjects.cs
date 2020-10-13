using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public class ModelVisitorRemoveMarkedIfNewObjects : ModelVisitorBase
    {
        //protected override void BeginVisit(IGroupDocuments cn)
        //{
        //}
        protected override void BeginVisit(IGroupListCatalogs cn)
        {
            var p = (GroupListCatalogs)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListCatalogs.ToList();
                foreach (var t in lst)
                {
                    if (t.IsHasMarkedForDeletion && t.IsHasNew)
                    {
                        p.ListCatalogs.Remove(t);
                    }
                }
            }
        }
        protected override void BeginVisit(IGroupListMainViewForms cn)
        {
            var p = (GroupListMainViewForms)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListMainViewForms.ToList();
                foreach (var t in lst)
                {
                    if (t.IsHasMarkedForDeletion && t.IsHasNew)
                    {
                        p.ListMainViewForms.Remove(t);
                    }
                }
            }
        }
        protected override void BeginVisit(IGroupListRoles cn)
        {
            var p = (GroupListRoles)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListRoles.ToList();
                foreach (var t in lst)
                {
                    if (t.IsHasMarkedForDeletion && t.IsHasNew)
                    {
                        p.ListRoles.Remove(t);
                    }
                }
            }
        }
        protected override void BeginVisit(IGroupListConstants cn)
        {
            var p = (GroupListConstants)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListConstants.ToList();
                foreach (var t in lst)
                {
                    if (t.IsHasMarkedForDeletion && t.IsHasNew)
                    {
                        p.ListConstants.Remove(t);
                    }
                }
            }
        }
        protected override void BeginVisit(IGroupListDocuments cn)
        {
            var p = (GroupListDocuments)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListDocuments.ToList();
                foreach (var t in lst)
                {
                    if (t.IsHasMarkedForDeletion && t.IsHasNew)
                    {
                        p.ListDocuments.Remove(t);
                    }
                }
            }
        }
        protected override void BeginVisit(IGroupListEnumerations cn)
        {
            var p = (GroupListEnumerations)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListEnumerations.ToList();
                foreach (var t in lst)
                {
                    if (t.IsHasMarkedForDeletion && t.IsHasNew)
                    {
                        p.ListEnumerations.Remove(t);
                    }
                }
            }
        }
        protected override void BeginVisit(IGroupListForms cn)
        {
            var p = (GroupListForms)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListForms.ToList();
                foreach (var t in lst)
                {
                    if (t.IsHasMarkedForDeletion && t.IsHasNew)
                    {
                        p.ListForms.Remove(t);
                    }
                }
            }
        }
        protected override void BeginVisit(IGroupListJournals cn)
        {
            var p = (GroupListJournals)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListJournals.ToList();
                foreach (var t in lst)
                {
                    if (t.IsHasMarkedForDeletion && t.IsHasNew)
                    {
                        p.ListJournals.Remove(t);
                    }
                }
            }
        }
        protected override void BeginVisit(IGroupListProperties cn)
        {
            var p = (GroupListProperties)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListProperties.ToList();
                foreach (var t in lst)
                {
                    if (t.IsHasMarkedForDeletion && t.IsHasNew)
                    {
                        p.ListProperties.Remove(t);
                    }
                }
            }
        }
        protected override void BeginVisit(IGroupListPropertiesTabs cn)
        {
            var p = (GroupListPropertiesTabs)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListPropertiesTabs.ToList();
                foreach (var t in lst)
                {
                    if (t.IsHasMarkedForDeletion && t.IsHasNew)
                    {
                        p.ListPropertiesTabs.Remove(t);
                    }
                }
            }
        }
        protected override void BeginVisit(IGroupListReports cn)
        {
            var p = (GroupListReports)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListReports.ToList();
                foreach (var t in lst)
                {
                    if (t.IsHasMarkedForDeletion && t.IsHasNew)
                    {
                        p.ListReports.Remove(t);
                    }
                }
            }
        }
    }
}
