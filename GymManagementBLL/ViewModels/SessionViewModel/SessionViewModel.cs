using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.SessionViewModel
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TrainetrName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }
        public int Capacity { get; set; }
        public int Availableslots { get; set; }

        #region ComputedProperty

        public string DateDisplay => $"{StartDate: MMM dd , yyyy}";

        public string TimeRangeDisplay => $"{StartDate: hh : mm tt} - {EndtDate: hh : mm tt}";

        public TimeSpan Duration => EndtDate - StartDate;

        public string Status
        {
            get {

                // if session upcoming => startdate > date.now
                if (StartDate > DateTime.Now)
                    return "Upcoming";
                else if (StartDate <= DateTime.Now && EndtDate >= DateTime.Now)
                    return "OnGoing";
                else
                    return " Completed";
                            
            }
        
        }

        #endregion



    }
}
