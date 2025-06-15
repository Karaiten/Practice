using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SoftwareStore.Models
{
    public class BookDbInitializer : DropCreateDatabaseAlways<SoftwareContext>
    {
        protected override void Seed(SoftwareContext db)
        {
            // Створення категорій
            var devTools = new Category { Name = "Development Tools" };
            var design = new Category { Name = "Design" };
            var ide = new Category { Name = "IDE" };
            var uiUx = new Category { Name = "UI/UX" };

            db.Categories.AddRange(new[] { devTools, design, ide, uiUx });
            db.SaveChanges(); // Обов’язково — щоб категорії отримали Id

            // Додавання програм із прив’язкою до категорій
            db.Softwares.AddRange(new[]
            {
                new Software
                {
                    Name = "Visual Studio",
                    Developer = "Microsoft",
                    Version = "2022",
                    Price = 0,
                    LicenseType = "Free",
                    Category = devTools
                },
                new Software
                {
                    Name = "Adobe Photoshop",
                    Developer = "Adobe",
                    Version = "2024",
                    Price = 299,
                    LicenseType = "Commercial",
                    Category = design
                },
                new Software
                {
                    Name = "JetBrains Rider",
                    Developer = "JetBrains",
                    Version = "2023.3",
                    Price = 149,
                    LicenseType = "Trial",
                    Category = ide
                },
                new Software
                {
                    Name = "Figma",
                    Developer = "Figma Inc.",
                    Version = "1.0",
                    Price = 0,
                    LicenseType = "Free",
                    Category = uiUx
                }
            });

            db.SaveChanges();

            base.Seed(db);
        }
    }
}
