using System.Data.Entity.Migrations;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;

namespace Yeast.Datalayer.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<YeastDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(YeastDbContext context)
        {
            context.Options.AddOrUpdate(op => new { op.Name, op.Value },
                new Option { Name = "Image1", Value = "/Content/upload/images/newletter-bg.jpg" },
                new Option { Name = "Image2", Value = "/Content/upload/images/slideshow_image_1-min-rs.jpg" },
                new Option { Name = "Image3", Value = "/Content/upload/images/slideshow_image_2-min.jpg" },
                new Option { Name = "TextImag1", Value = "بازرگانی مهاجر" },
                new Option { Name = "TextImag2", Value = "بازرگانی مهاجر1" },
                new Option { Name = "TextImag3", Value = "بازرگانی مهاجر2" }
                );

            context.Options.AddOrUpdate(op => new { op.Name, op.Value },
                new Option { Name = "TitleFa", Value = "33333dsfgg" },
                new Option { Name = "BodyFa", Value = "dddd1" },
                new Option { Name = "TitleEn", Value = "dddsa" },
                new Option { Name = "BodyEn", Value = "sdd1" },
                new Option { Name = "TitleSa", Value = "sdfsdfs" },
                new Option { Name = "BodySa", Value = "dsdsdsd1" },
                new Option { Name = "TitleRu", Value = "hgjgjh" },
                new Option { Name = "BodyRu", Value = "gfhf1dfgdfgdg" },
                new Option { Name = "TitleFa1", Value = "33333dsfgg" },
                new Option { Name = "BodyFa1", Value = "dddd1" },
                new Option { Name = "TitleEn1", Value = "dddsa" },
                new Option { Name = "BodyEn1", Value = "sdd1" },
                new Option { Name = "TitleSa1", Value = "sdfsdfs" },
                new Option { Name = "BodySa1", Value = "dsdsdsd1" },
                new Option { Name = "TitleRu1", Value = "hgjgjh" },
                new Option { Name = "BodyRu1", Value = "gfhf1" }
                );

            context.SaveChanges();

            //context.Users.AddOrUpdate(u => u.UserName, new User
            //{
            //    RegisterDate = DateTime.Now,
            //    IsBaned = false,
            //    RegisterType = UserRegisterType.Active,
            //    PhoneNumber = "09146208938",
            //    Password = Encryption.EncryptingPassword("09146208938"),
            //    Role = context.Roles.Find(1),
            //    UserName = "admin"
            //});

            base.Seed(context);
        }
    }
}