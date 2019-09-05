using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForThePeople.Models
{
    public class Legislature
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Sponser { get; set; }
        public string PoliticalPartyId { get; set; }
        public string SponsorState { get; set; }
        public DateTime DateProposed { get; set; }
        public string PrimarySubject { get; set; }
        public string Summary { get; set; }
        public DateTime LatestMajorActionDate { get; set; }
        public string LatestMajorActionDescription { get; set; }
        public string GovTrackUrl { get; set; }
    }
}
