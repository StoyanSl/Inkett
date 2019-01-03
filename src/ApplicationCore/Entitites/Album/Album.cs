﻿using Ardalis.GuardClauses;
using Inkett.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;

namespace Inkett.ApplicationCore.Entitites
{
    public class Album : BaseEntity, IProfileAuthorizable
    {
        private const string defaultPictureUri = "https://res.cloudinary.com/inkettimgs/image/upload/v1545352616/rc7oscdwtwzpg6aheec8.jpg";
        private const string defaultAlbumTitle = "My Tattoos";
        private const string defaultAlbumDescription = "Tattoos that I currently have.";
        public Album()
        {

        }
        public Album(int profileId)
        {
            this.ProfileId = profileId;
            this.Title = defaultAlbumTitle;
            this.Description = defaultAlbumDescription;
            this.AlbumPictureUri = defaultPictureUri;
        }
        public Album(int profileId, string title, string description, string pictureUri= defaultPictureUri)
        {
            Guard.Against.NullOrEmpty(title, nameof(title));
            this.ProfileId = profileId;
            this.Title = title;
            this.Description = description;
            this.AlbumPictureUri = pictureUri??defaultPictureUri;
        }


        public int ProfileId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AlbumPictureUri { get; set; }

        public List<Tattoo> Tattoos { get; set; }
    }
}
