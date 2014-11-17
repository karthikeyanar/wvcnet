using WVC.Api.Repository;
using WVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WVC.Models;
using WVC.Contracts;

namespace WVC.Api.Controllers {

    [Authorize(Roles = "member")]
    [RoutePrefix("WoodVolume")]
    public class WoodVolumeController:BaseApiController<WoodVolume,wvc_wood_volume> {

        public WoodVolumeController()
            : this(new WoodVolumeRepository(),
            new DivisionRepository(),
            new DistrictRepository(),
            new RangeRepository(),
            new TalukRepository(),
            new VillageRepository()) {
        }

        public WoodVolumeController(IWoodVolumeRepository woodVolumeRepository
            ,DivisionRepository divisionRepository
            ,DistrictRepository districtRepository
            ,RangeRepository rangeRepository
            ,TalukRepository talukRepository
            ,VillageRepository villageRepository) {
            _WoodVolumeRepository = woodVolumeRepository;
            _DivisionRepository = divisionRepository;
            _DistrictRepository = districtRepository;
            _RangeRepository = rangeRepository;
            _TalukRepository = talukRepository;
            _VillageRepository = villageRepository;
        }

        IWoodVolumeRepository _WoodVolumeRepository;
        IDivisionRepository _DivisionRepository;
        IDistrictRepository _DistrictRepository;
        IRangeRepository _RangeRepository;
        ITalukRepository _TalukRepository;
        IVillageRepository _VillageRepository;

        public override WoodVolume Get(int? id) {
            return _WoodVolumeRepository.Get(new WoodVolume { id = id },new Paging { }).rows.FirstOrDefault();
        }

        public override PaginatedListResult<WoodVolume> Search([FromUri] WoodVolume criteria,[FromUri] Paging paging) {
            return _WoodVolumeRepository.Get(criteria,paging);
        }

        //[HttpGet]
        //[ActionName("Select")]
        //public List<AutoCompleteList> GetWoodVolumes([FromUri] string term, [FromUri] int pageSize = 10) {
        //	return _WoodVolumeRepository.GetWoodVolumes(term, pageSize);
        //}

        [Authorize(Roles = "member")]
        public override IHttpActionResult Post(WoodVolume contract) {
            if(contract == null) {
                return BadRequest("Contract is null");
            }
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            contract = SetUpContract(contract);
            var saveContract = Repository.Save(contract);
            if(saveContract.Errors == null) {
                return Ok(this.Get(saveContract.id));
            } else {
                int index = 0;
                foreach(var err in saveContract.Errors) {
                    index++;
                    ModelState.AddModelError("Error" + index,err.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "member")]
        public override IHttpActionResult Put(int id,WoodVolume contract) {
            if(contract == null) {
                return BadRequest("Contract is null");
            }
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            contract = SetUpContract(contract);
            contract.id = id;
            var saveContract = Repository.Save(contract);
            if(saveContract.Errors == null) {
                return Ok(this.Get(saveContract.id));
            } else {
                int index = 0;
                foreach(var err in saveContract.Errors) {
                    index++;
                    ModelState.AddModelError("Error" + index,err.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
        }

        private WoodVolume SetUpContract(WoodVolume contract) {
            if((contract.division_id ?? 0) <= 0) {
                if(string.IsNullOrEmpty(contract.division_name) == false) {
                    BaseRepository<Division,wvc_division> divisionRepository = new BaseRepository<Division,wvc_division>();
                    Division division = divisionRepository.Find(new Division { name = contract.division_name }).FirstOrDefault();
                    if(division != null) {
                        contract.division_id = division.id;
                    } else {
                        division = divisionRepository.Save(new Division { name = contract.division_name });
                        if(division != null) {
                            contract.division_id = division.id;
                        }
                    }
                }
            }
            if((contract.district_id ?? 0) <= 0) {
                if(string.IsNullOrEmpty(contract.district_name) == false) {
                    BaseRepository<District,wvc_district> districtRepository = new BaseRepository<District,wvc_district>();
                    District district = districtRepository.Find(new District { name = contract.district_name }).FirstOrDefault();
                    if(district != null) {
                        contract.district_id = district.id;
                    } else {
                        district = districtRepository.Save(new District { name = contract.district_name });
                        if(district != null) {
                            contract.district_id = district.id;
                        }
                    }
                }
            }
            if((contract.range_id ?? 0) <= 0) {
                if(string.IsNullOrEmpty(contract.range_name) == false) {
                    BaseRepository<Range,wvc_range> rangeRepository = new BaseRepository<Range,wvc_range>();
                    Range range = rangeRepository.Find(new Range { name = contract.range_name }).FirstOrDefault();
                    if(range != null) {
                        contract.range_id = range.id;
                    } else {
                        range = rangeRepository.Save(new Range { name = contract.range_name });
                        if(range != null) {
                            contract.range_id = range.id;
                        }
                    }
                }
            }
            if((contract.taluk_id ?? 0) <= 0) {
                if(string.IsNullOrEmpty(contract.taluk_name) == false) {
                    BaseRepository<Taluk,wvc_taluk> talukRepository = new BaseRepository<Taluk,wvc_taluk>();
                    Taluk taluk = talukRepository.Find(new Taluk { name = contract.taluk_name }).FirstOrDefault();
                    if(taluk != null) {
                        contract.taluk_id = taluk.id;
                    } else {
                        taluk = talukRepository.Save(new Taluk { name = contract.taluk_name });
                        if(taluk != null) {
                            contract.taluk_id = taluk.id;
                        }
                    }
                }
            }
            if((contract.village_id ?? 0) <= 0) {
                if(string.IsNullOrEmpty(contract.village_name) == false) {
                    BaseRepository<Village,wvc_village> villageRepository = new BaseRepository<Village,wvc_village>();
                    Village village = villageRepository.Find(new Village { name = contract.village_name }).FirstOrDefault();
                    if(village != null) {
                        contract.village_id = village.id;
                    } else {
                        village = villageRepository.Save(new Village { name = contract.village_name });
                        if(village != null) {
                            contract.village_id = village.id;
                        }
                    }
                }
            }
            return contract;
        }

        [Authorize(Roles = "member")]
        public override IHttpActionResult Delete(int id) {
            return base.Delete(id);
        }
    }
}