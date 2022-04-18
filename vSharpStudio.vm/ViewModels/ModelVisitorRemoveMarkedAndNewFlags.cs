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
        protected override void EndVisit(IGroupListCatalogs cn)
        {
            var p = (GroupListCatalogs)cn;
            foreach (var t in p.ListCatalogs)
            {
                t.IsMarkedForDeletion = false;
                t.IsNew = false;
            }
        }
        protected override void EndVisit(IGroupListMainViewForms cn)
        {
            var p = (GroupListMainViewForms)cn;
            foreach (var t in p.ListMainViewForms)
            {
                t.IsMarkedForDeletion = false;
                t.IsNew = false;
            }
        }
        protected override void EndVisit(IGroupListRoles cn)
        {
            var p = (GroupListRoles)cn;
            foreach (var t in p.ListRoles)
            {
                t.IsMarkedForDeletion = false;
                t.IsNew = false;
            }
        }
        protected override void EndVisit(IGroupListConstants cn)
        {
            var p = (GroupListConstants)cn;
            foreach (var t in p.ListConstants)
            {
                t.IsMarkedForDeletion = false;
                t.IsNew = false;
            }
        }
        protected override void EndVisit(IGroupListDocuments cn)
        {
            var p = (GroupListDocuments)cn;
            foreach (var t in p.ListDocuments)
            {
                t.IsMarkedForDeletion = false;
                t.IsNew = false;
            }
        }
        protected override void EndVisit(IGroupListEnumerations cn)
        {
            var p = (GroupListEnumerations)cn;
            foreach (var t in p.ListEnumerations)
            {
                t.IsMarkedForDeletion = false;
                t.IsNew = false;
            }
        }
        protected override void EndVisit(IEnumeration en)
        {
            var p = (Enumeration)en;
            foreach (var t in p.ListEnumerationPairs)
            {
                t.IsMarkedForDeletion = false;
                t.IsNew = false;
            }
            p.IsMarkedForDeletion = false;
            p.IsNew = false;
        }
        protected override void EndVisit(IGroupListForms cn)
        {
            var p = (GroupListForms)cn;
            foreach (var t in p.ListForms)
            {
                t.IsMarkedForDeletion = false;
                t.IsNew = false;
            }
        }
        protected override void EndVisit(IGroupListJournals cn)
        {
            var p = (GroupListJournals)cn;
            foreach (var t in p.ListJournals)
            {
                t.IsMarkedForDeletion = false;
                t.IsNew = false;
            }
        }
        protected override void EndVisit(IGroupListProperties cn)
        {
            var p = (GroupListProperties)cn;
            foreach (var t in p.ListProperties)
            {
                t.IsMarkedForDeletion = false;
                t.IsNew = false;
            }
        }
        protected override void EndVisit(IGroupListDetails cn)
        {
            var p = (GroupListDetails)cn;
            foreach (var t in p.ListDetails)
            {
                t.IsMarkedForDeletion = false;
                t.IsNew = false;
            }
        }
        protected override void EndVisit(IGroupListReports cn)
        {
            var p = (GroupListReports)cn;
            foreach (var t in p.ListReports)
            {
                t.IsMarkedForDeletion = false;
                t.IsNew = false;
            }
        }
        protected override void EndVisit(IGroupListBaseConfigLinks en)
        {
            var p = (GroupListBaseConfigLinks)en;
            foreach (var t in p.ListBaseConfigLinks)
            {
                t.IsMarkedForDeletion = false;
                t.IsNew = false;
            }
        }
        protected override void EndVisit(IGroupListAppSolutions en)
        {
            var p = (GroupListAppSolutions)en;
            foreach (var t in p.ListAppSolutions)
            {
                foreach (var tt in t.ListAppProjects)
                {
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        ttt.IsMarkedForDeletion = false;
                        ttt.IsNew = false;
                    }
                    tt.IsMarkedForDeletion = false;
                    tt.IsNew = false;
                }
                t.IsMarkedForDeletion = false;
                t.IsNew = false;
            }
        }
    }
}
