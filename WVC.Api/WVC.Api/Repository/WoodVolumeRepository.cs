using WVC.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using WVC.Contracts;
using WVC.Models;

namespace WVC.Api.Repository {
    public interface IWoodVolumeRepository {
        PaginatedListResult<WoodVolume> Get(WoodVolume criteria,Paging paging);
    }

    public class WoodVolumeRepository:IWoodVolumeRepository {

        public PaginatedListResult<WoodVolume> Get(WoodVolume criteria,Paging paging) {
            using(WVCContext context = new WVCContext()) {
                IQueryable<wvc_wood_volume> woodVolumes = context.wvc_wood_volume;
                if((criteria.id ?? 0) > 0) {
                    woodVolumes = woodVolumes.Where(q => q.id == criteria.id);
                }
                if(string.IsNullOrEmpty(criteria.name) == false) {
                    woodVolumes = woodVolumes.Where(q => q.name.StartsWith(criteria.name));
                }
                if((criteria.division_id ?? 0) > 0) {
                    woodVolumes = woodVolumes.Where(q => q.division_id == criteria.division_id);
                }
                if((criteria.district_id ?? 0) > 0) {
                    woodVolumes = woodVolumes.Where(q => q.district_id == criteria.district_id);
                }
                if((criteria.range_id ?? 0) > 0) {
                    woodVolumes = woodVolumes.Where(q => q.range_id == criteria.range_id);
                }
                if((criteria.village_id ?? 0) > 0) {
                    woodVolumes = woodVolumes.Where(q => q.village_id == criteria.village_id);
                }
                if((criteria.taluk_id ?? 0) > 0) {
                    woodVolumes = woodVolumes.Where(q => q.taluk_id == criteria.taluk_id);
                }
                IQueryable<WoodVolume> query = (from volume in woodVolumes
                                                join div in context.divisions on volume.division_id equals div.id into divisions
                                                from div in divisions.DefaultIfEmpty()
                                                join dis in context.districts on volume.division_id equals dis.id into districts
                                                from dis in districts.DefaultIfEmpty()
                                                join ran in context.ranges on volume.range_id equals ran.id into ranges
                                                from ran in ranges.DefaultIfEmpty()
                                                join tal in context.taluks on volume.taluk_id equals tal.id into taluks
                                                from tal in taluks.DefaultIfEmpty()
                                                join vil in context.villages on volume.village_id equals vil.id into villages
                                                from vil in villages.DefaultIfEmpty()
                                                select new WoodVolume {
                                                    id = volume.id,
                                                    description = volume.description,
                                                    district_id = volume.district_id,
                                                    district_name = dis.name,
                                                    division_id = volume.division_id,
                                                    division_name = div.name,
                                                    name = volume.name,
                                                    range_id = volume.range_id,
                                                    range_name = ran.name,
                                                    taluk_id = volume.taluk_id,
                                                    taluk_name = tal.name,
                                                    user_id = volume.user_id,
                                                    village_id = volume.village_id,
                                                    village_name = vil.name
                                                });
                paging.Total = query.Count();
                if(string.IsNullOrEmpty(paging.SortOrder)) {
                    paging.SortOrder = "asc";
                }
                if(string.IsNullOrEmpty(paging.SortName) == false) {
                    query = query.OrderBy(paging.SortName,(paging.SortOrder == "asc"));
                }
                if(paging.PageSize > 0) {
                    query = query.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize);
                }

                PaginatedListResult<WoodVolume> paginatedList = new PaginatedListResult<WoodVolume>();
                paginatedList.rows = query.ToList();
                paginatedList.total = paging.Total;
                if((criteria.id ?? 0) > 0) {
                    WoodVolume woodVolume = paginatedList.rows.FirstOrDefault();
                    if(woodVolume != null) {
                        woodVolume.items = (from item in context.wvc_wood_volum_item
                                            where item.wood_volume_id == woodVolume.id
                                            select new WoodVolumeItem {
                                                co_efficient = item.co_efficient,
                                                description = item.description,
                                                final_volume = item.final_volume,
                                                girth = item.girth,
                                                length = item.length,
                                                volume = item.volume,
                                                wood_volume_id = item.wood_volume_id,
                                                id = item.id
                                            }).ToList();
                    }
                }
                return paginatedList;
            }
        }

    }
}