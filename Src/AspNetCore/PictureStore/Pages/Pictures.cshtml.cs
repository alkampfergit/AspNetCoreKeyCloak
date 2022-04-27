using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using PictureStore.Common;
using PictureStore.Common.Entities;
using System.Security.Cryptography;

namespace PictureStore.Pages
{
    [Authorize]
    public class PicturesModel : PageModel
    {
        private readonly PictureStoreContext _context;
        private readonly IOptionsMonitor<PictureStoreConfiguration> _config;

        public PicturesModel(
            PictureStoreContext context,
            IOptionsMonitor<PictureStoreConfiguration> config)
        {
            _context = context;
            _config = config;
        }

        public void OnGet()
        {
            LoadPictures();
        }

        private void LoadPictures()
        {
            Pictures = _context.Pictures
                .ToList();
        }

        public List<Picture> Pictures { get; set; }

        [BindProperty]
        public IFormFile Upload { get; set; }

        [BindProperty]
        public string Title { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = BitConverter.ToString(RandomNumberGenerator.GetBytes(16)).Replace("-", "");
            var fileName = Path.Combine(
                _config.CurrentValue.BaseImageDirectory,
                id);
            using var fileStream = new FileStream(fileName, FileMode.Create);
            await Upload.CopyToAsync(fileStream);
            var picture = new Picture()
            {
                Id = id,
                FileName = fileName,
                Title = Title,
                UserId = "blah"
            };
            _context.Pictures.Add(picture);
            await _context.SaveChangesAsync();

            LoadPictures();
            ClearPage();
            return Page();
        }

        private void ClearPage()
        {
            Title = "";
        }
    }
}
