using AutoMapper;
using Inkett.Web.Common.Mapping;
using System.ComponentModel.DataAnnotations;
using Profile = Inkett.ApplicationCore.Entitites.Profile;

namespace Inkett.Web.Models.ViewModels.Profiles
{
    public class ProfileViewModel : IMapFrom<Profile>
    {
        public int Id { get; set; }

        [Display(Name = "Profile Name")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        public string ProfilePictureUri { get; set; }

        public string CoverPictureUri { get; set; }

        public bool IsFollowed { get; set; }

        public bool IsOwner { get; set; }

      
    }
}
