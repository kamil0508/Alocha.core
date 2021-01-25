using Alocha.WebUi.Models.SowVM;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Helpers
{
    public static class DataTablesHelper
    {
        public static List<SowVM> FilterData(ref DTSettingVM dtModel, IEnumerable<SowVM> sows)
        {
            //SEARCHING...
            if (!string.IsNullOrEmpty(dtModel.NumberFilter) && sows != null)
            {
                string s = dtModel.NumberFilter;
                sows = sows.Where(a => a.Number.Contains(s)).ToList();
            }
            //SORTING...  
            if (!(string.IsNullOrEmpty(dtModel.SortColumn) && string.IsNullOrEmpty(dtModel.SortColumnDir)))
            {
                if (sows != null)
                {
                    switch (dtModel.SortColumnDir)
                    {
                        case "asc":
                            sows = SortDataAscending(sows.ToList(), dtModel.SortColumn);
                            break;
                        case "desc":
                            sows = SortDataDescending(sows.ToList(), dtModel.SortColumn);
                            break;
                    }
                }
            }

            dtModel.PageSize = dtModel.Lenght != null ? int.Parse(dtModel.Lenght) : 0;
            dtModel.Skip = dtModel.Start != null ? int.Parse(dtModel.Start) : 0;
           
            dtModel.RecordsTotal = sows.Count();
            var data = sows.Skip(dtModel.Skip).Take(dtModel.PageSize).ToList();

            return data;
        }

        public static DTSettingVM BindRequestForm(IFormCollection form)
        {
            var dtModel = new DTSettingVM();
            dtModel.Draw = form["draw"].FirstOrDefault();

            dtModel.Start = form["start"].FirstOrDefault();
            dtModel.Lenght = form["length"].FirstOrDefault();

            //sorting
            dtModel.SortColumn = form["columns[" + form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            dtModel.SortColumnDir = form["order[0][dir]"].FirstOrDefault();

            dtModel.NumberFilter = form["columns[0][search][value]"].FirstOrDefault();

            return dtModel;
        }

        private static List<SowVM> SortDataAscending(List<SowVM> list, string columnName)
        {
            switch (columnName)
            {
                case "Number":
                    list = list.OrderBy(c => c.Number).ToList();
                    break;
                case "Status":
                    list = list.OrderBy(c => c.Status).ToList();
                    break;
                case "DateHappening":
                    list = list.OrderBy(c => c.DateHappening).ToList();
                    break;
                case "DateInsimination":
                    list = list.OrderBy(c => c.DateInsimination).ToList();
                    break;
                case "DateDetachment":
                    list = list.OrderBy(c => c.DateDetachment).ToList();
                    break;
                case "DateBorn":
                    list = list.OrderBy(c => c.DateBorn).ToList();
                    break;
                case "VaccinateDate":
                    list = list.OrderBy(c => c.VaccineDate).ToList();
                    break;
                default:
                    break;
            }

            return list;
        }

        private static List<SowVM> SortDataDescending(List<SowVM> list, string columnName)
        {
            switch (columnName)
            {
                case "Number":
                    list = list.OrderByDescending(c => c.Number).ToList();
                    break;
                case "Status":
                    list = list.OrderByDescending(c => c.Status).ToList();
                    break;
                case "DateHappening":
                    list = list.OrderByDescending(c => c.DateHappening).ToList();
                    break;
                case "DateInsimination":
                    list = list.OrderByDescending(c => c.DateInsimination).ToList();
                    break;
                case "DateDetachment":
                    list = list.OrderByDescending(c => c.DateDetachment).ToList();
                    break;
                case "DateBorn":
                    list = list.OrderByDescending(c => c.DateBorn).ToList();
                    break;
                case "VaccinateDate":
                    list = list.OrderByDescending(c => c.VaccineDate).ToList();
                    break;
                default:
                    break;
            }

            return list;
        }
    }
}
