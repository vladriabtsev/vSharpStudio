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
        protected override void EndVisit(IGroupListCatalogs cn)
        {
            var p = (GroupListCatalogs)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListCatalogs.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion && t.IsNew)
                    {
                        p.ListCatalogs.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IGroupListMainViewForms cn)
        {
            var p = (GroupListMainViewForms)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListMainViewForms.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion && t.IsNew)
                    {
                        p.ListMainViewForms.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IGroupListRoles cn)
        {
            var p = (GroupListRoles)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListRoles.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion && t.IsNew)
                    {
                        p.ListRoles.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IGroupListConstants cn)
        {
            var p = (GroupListConstants)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListConstants.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion && t.IsNew)
                    {
                        p.ListConstants.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IGroupListDocuments cn)
        {
            var p = (GroupListDocuments)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListDocuments.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion && t.IsNew)
                    {
                        p.ListDocuments.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IGroupListEnumerations cn)
        {
            var p = (GroupListEnumerations)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListEnumerations.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion && t.IsNew)
                    {
                        p.ListEnumerations.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IGroupListForms cn)
        {
            var p = (GroupListForms)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListForms.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion && t.IsNew)
                    {
                        p.ListForms.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IGroupListJournals cn)
        {
            var p = (GroupListJournals)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListJournals.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion && t.IsNew)
                    {
                        p.ListJournals.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IGroupListProperties cn)
        {
            var p = (GroupListProperties)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListProperties.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion && t.IsNew)
                    {
                        p.ListProperties.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IGroupListPropertiesTabs cn)
        {
            var p = (GroupListPropertiesTabs)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListPropertiesTabs.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion && t.IsNew)
                    {
                        p.ListPropertiesTabs.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IGroupListReports cn)
        {
            var p = (GroupListReports)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListReports.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion && t.IsNew)
                    {
                        p.ListReports.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IEnumeration en)
        {
            var p = (Enumeration)en;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListEnumerationPairs.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion && t.IsNew)
                    {
                        p.ListEnumerationPairs.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IGroupDocuments cn)
        {
            var p = (GroupDocuments)cn;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.GroupListDocuments.ListDocuments.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion && t.IsNew)
                    {
                        p.GroupListDocuments.ListDocuments.Remove(t);
                    }
                }
                var lst2 = p.GroupSharedProperties.ListProperties.ToList();
                foreach (var t in lst2)
                {
                    if (t.IsMarkedForDeletion && t.IsNew)
                    {
                        p.GroupSharedProperties.ListProperties.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IGroupListAppSolutions cn)
        {
            var p = (GroupListAppSolutions)cn;
            if (p.IsHasMarkedForDeletion)
            {
                var lst = p.ListAppSolutions.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion)
                    {
                        p.ListAppSolutions.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IAppSolution cn)
        {
            var p = (AppSolution)cn;
            if (p.IsHasMarkedForDeletion)
            {
                var lst = p.ListAppProjects.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion)
                    {
                        p.ListAppProjects.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IAppProject cn)
        {
            var p = (AppProject)cn;
            if (p.IsHasMarkedForDeletion)
            {
                var lst = p.ListAppProjectGenerators.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion)
                    {
                        p.ListAppProjectGenerators.Remove(t);
                    }
                }
            }
        }
        protected override void EndVisit(IGroupListBaseConfigLinks en)
        {
            var p = (GroupListBaseConfigLinks)en;
            if (p.IsHasMarkedForDeletion && p.IsHasNew)
            {
                var lst = p.ListBaseConfigLinks.ToList();
                foreach (var t in lst)
                {
                    if (t.IsMarkedForDeletion && t.IsNew)
                    {
                        p.ListBaseConfigLinks.Remove(t);
                    }
                }
            }
        }
    }
}
