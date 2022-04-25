using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PictureStore.Common.Entities
{
    public class Picture
    {
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Id of the user that own the image.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string UserId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string FileName { get; set; }
    }
}
