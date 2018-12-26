using Inkett.ApplicationCore.Entitites;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inkett.Infrastructure.Data
{
    public class InkettContextSeed
    {
        public static async Task SeedAsync(InkettContext inkettContext)
        {
            if (!inkettContext.Styles.Any())
            {
                inkettContext.Styles.AddRange(GetPreconfiguredStyles());

                await inkettContext.SaveChangesAsync();
            }
        }

        static  IEnumerable<Style> GetPreconfiguredStyles()
        {
            return new List<Style>()
            {
              new Style() { Name = "Abstract"},
              new Style() { Name = "Ambigram"},
              new Style() { Name = "Armband"},
              new Style() { Name = "Baroque"},
              new Style() { Name = "Bio-mechanical"},
              new Style() { Name = "Bio-organic"},
              new Style() { Name = "Black & Grey"},
              new Style() { Name = "Blackwork"},
              new Style() { Name = "Body Mods"},
              new Style() { Name = "Botanical"},
              new Style() { Name = "Cartoons"},
              new Style() { Name = "Celtic"},
              new Style() { Name = "Chinese"},
              new Style() { Name = "Classic"},
              new Style() { Name = "Color"},
              new Style() { Name = "Couples"},
              new Style() { Name = "Cover up"},
              new Style() { Name = "Dotwork"},
              new Style() { Name = "Expressionism"},
              new Style() { Name = "Fantasy"},
              new Style() { Name = "Flash"},
              new Style() { Name = "Geometric"},
              new Style() { Name = "Graffiti"},
              new Style() { Name = "Illusion"},
              new Style() { Name = "Illustrative"},
              new Style() { Name = "Japanese"},
              new Style() { Name = "Lettering"},
              new Style() { Name = "Linework"},
              new Style() { Name = "Macabre"},
              new Style() { Name = "Maori"},
              new Style() { Name = "Mashup"},
              new Style() { Name = "Memorial"},
              new Style() { Name = "Minimalism"},
              new Style() { Name = "Neo-Traditional"},
              new Style() { Name = "New School"},
              new Style() { Name = "Old school"},
              new Style() { Name = "Ornamental"},
              new Style() { Name = "Piercing"},
              new Style() { Name = "Polynesian"},
              new Style() { Name = "Portraits"},
              new Style() { Name = "Quote"},
              new Style() { Name = "Realistic"},
              new Style() { Name = "Red Ink"},
              new Style() { Name = "Religious"},
              new Style() { Name = "Ring"},
              new Style() { Name = "Samoan"},
              new Style() { Name = "Scarification"},
              new Style() { Name = "Script"},
              new Style() { Name = "Sexy"},
              new Style() { Name = "Stonework"},
              new Style() { Name = "Surrealism"},
              new Style() { Name = "Traditional"},
              new Style() { Name = "Trash Polka"},
              new Style() { Name = "Tribal"},
              new Style() { Name = "UV light"},
              new Style() { Name = "Watercolor"}
            };
        }
    }
}
