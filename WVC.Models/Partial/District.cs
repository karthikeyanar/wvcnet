namespace WVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class District
    {
        public IEnumerable<ErrorInfo> Save()
        {
            IEnumerable<ErrorInfo> errors = ValidationHelper.Validate(this);
            if (errors.Any())
            {
                return errors;
            }
            //Deal
            using (WVCContext context = new WVCContext())
            {
                if (this.Id > 0)
                {
                    this.LastUpdatedBy = 1;
                    this.LastUpdatedDate = DateTime.Now;
                    context.Entry(this).State = EntityState.Modified;
                }
                else
                {
                    this.CreatedBy = 1;
                    this.CreatedDate = DateTime.Now;
                    context.Districts.Add(this);
                }
                context.SaveChanges();
            }
            return null;
        }
    }
}
