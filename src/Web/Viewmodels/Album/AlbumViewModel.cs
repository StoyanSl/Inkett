﻿using Inkett.Web.Attributes.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.Web.Viewmodels.Album
{
    public class AlbumViewModel
    {
        public int Id { get; set; }

        [AlbumTitle]
        [Display(Name = "Album Title")]
        public string Title { get; set; }

        [ImageValidation]
        [Display(Name = "Album Picture")]
        public IFormFile Picture { get; set; }

        [Display(Name = "Album Description")]
        public string Description { get; set; }

        public string PictureUri { get; set; }
        

    }
}
