<div class="col-md-4">
               <div class="form-group">
                   <label class="col-md-2 control-label">
                       Village
                   </label>
                   <div class="col-md-10">
                       <input type="text" class="form-control input-sm" name="village_name" data-bind="value:model().village_name,ajaxComboBox: { inputValue: model().village_name(),source: model().searchVillage, select: model().selectVillage,change: model().changeVillage }" placeholder="Village" />
                       <input type="hidden" name="village_id" data-bind="value:model().village_id" />
                   </div>
               </div>
           </div>