using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Wineapp.Models;
using Wineapp.Services;

namespace Wineapp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITastes _tasteServices;
        private readonly IFilters _filtersServices;


        public ConfirmEmailModel(UserManager<AppUser> userManager, ITastes tasteServices, IFilters filtersServices)
        {
            _userManager = userManager;
            _tasteServices = tasteServices;
            _filtersServices = filtersServices;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {

            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            foreach (Colour colour in await _filtersServices.GetColourAsync())
            {
                ColourTaste colourTaste = new ColourTaste();
                colourTaste.ColourId = colour.Id;
                colourTaste.AppUserId = userId;
                colourTaste.Score = 0;
                await _tasteServices.CreateColourTasteAsync(colourTaste);
            }
            foreach (Source source in await _filtersServices.GetSourceAsync())
            {
                SourceTaste sourceTaste = new SourceTaste();
                sourceTaste.SourceId = source.Id;                
                sourceTaste.AppUserId = userId;
                sourceTaste.Score = 0;
                await _tasteServices.CreateSourceTasteAsync(sourceTaste);
            }
            foreach (Sweetnes sweetnes in await _filtersServices.GetSweetnesAsync())
            {
                SweetnessTaste sweetnessTaste = new SweetnessTaste();
                sweetnessTaste.SweetnesId= sweetnes.Id;               
                sweetnessTaste.AppUserId = userId;
                sweetnessTaste.Score = 0;
                await _tasteServices.CreateSweetnessTasteAsync(sweetnessTaste);

            }


            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return Page();
        }
    }
}
