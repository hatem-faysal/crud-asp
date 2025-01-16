using crud2.Models;

namespace crud2.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder builder)
        {
            using (var applicationservices = builder.ApplicationServices.CreateScope())
            {
                var context = applicationservices.ServiceProvider.GetService<EcommerceDbContext>();
                context.Database.EnsureCreated();
                if (!context.Categories.Any())
                {
                    var caategoreies = new List<Category>()
                    {
                        new Category()
                        {
                            Name="c1",
                        },
                        new Category()
                        {
                            Name="c2",
                        },
                        new Category()
                        {
                            Name="c3",
                        },
                    };
                    context.Categories.AddRange(caategoreies);
                    context.SaveChanges();
                }

            }
        }
    }
}
