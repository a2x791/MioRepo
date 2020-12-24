using Microsoft.EntityFrameworkCore;
using Mio.Models.Relations;
using Mio.Models.Sale;
using Mio.Models.Text;
using Mio.Models.Time;
using Mio.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.API.Context
{
    public class MioContext : DbContext
    {
        public MioContext(DbContextOptions<MioContext> options)
           : base(options)
        {

        }

        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TextContent> TextContents { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserTextReactions> UserTextReactions { get; set; }
        public DbSet<Timeslot> Timeslots{ get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<ProductRentals> ProductRentals { get; set; }
        public DbSet<ProductHistory> ProductHistories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceTimeslot> ServiceTimeslots { get; set; }
        public DbSet<UserTimeslots> UserTimeslots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Addresses
            Address a1 = new Address { ID = 1, AddressLine1 = "22B Baker Street", AddressLine2 = "", City = "London", State = "London", Country = "England", Zipcode = 5201, UserID = "6f942aca-57a7-4adb-a6b5-5d9796cdf10b" };
            Address a2 = new Address { ID = 2, AddressLine1 = "400 South Orange Ave", AddressLine2 = "", City = "South Orange", State = "New Jersey", Country = "USA", Zipcode = 07079, UserID = "201d53a8-1fe6-41ef-aac4-aad4af8414cd" };

            modelBuilder.Entity<Address>().HasData(a1);
            modelBuilder.Entity<Address>().HasData(a2);

            //Account Type
            modelBuilder.Entity<AccountType>().HasData(new AccountType { ID = 1, Name = "General" });
            modelBuilder.Entity<AccountType>().HasData(new AccountType { ID = 2,  Name = "Seller" });
            modelBuilder.Entity<AccountType>().HasData(new AccountType { ID = 3, Name = "Admin" });
            modelBuilder.Entity<AccountType>().HasData(new AccountType { ID = 4, Name = "Server" });

            //Timeslot
            string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

            int k = 0;

            foreach (var day in days) {
                for (int i = 0; i < 24; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        var minutes = (j % 2 == 0) ? "00" : "30";
                        modelBuilder.Entity<Timeslot>().HasData(new Timeslot {ID = k+1,  StartTime = i.ToString() + ":" + minutes, Day = day });
                        k += 1;
                    }
                }
            }

            //ServiceTimeslot
            modelBuilder.Entity<ServiceTimeslot>().HasData(new ServiceTimeslot { ID = 1, ServiceID = 3, Duration = 1.5, Date = new DateTime(2021, 2, 20), TimeslotID = 260, CustomerID = "201d53a8-1fe6-41ef-aac4-aad4af8414cd", ServerID = "6f942aca-57a7-4adb-a6b5-5d9796cdf10b"});

            //Product Types
            modelBuilder.Entity<ProductType>().HasData(new ProductType { ID = 1, Name = "Cups" });
            modelBuilder.Entity<ProductType>().HasData(new ProductType { ID = 2, Name = "Energy Drink" });
            modelBuilder.Entity<ProductType>().HasData(new ProductType { ID = 3, Name = "Book" });
            modelBuilder.Entity<ProductType>().HasData(new ProductType { ID = 4, Name = "Legal Help" });

            //Product Option
            modelBuilder.Entity<ProductOption>().HasData(new ProductOption { ID = 1, ImagePaths = "images/product/201d53a8-1efs-41ef-aac4-aadsff8414cd/cup.jpg", OptionLabel1 = "Color", Options1="Blue,Black"});
            modelBuilder.Entity<ProductOption>().HasData(new ProductOption { ID = 2, ImagePaths = "images/product/201d53a8-1efs-41ef-aac4-aadsff8414cd/redbull.jpg", OptionLabel1 = "Size", Options1 = "8.0fl,12.0fl" });
            modelBuilder.Entity<ProductOption>().HasData(new ProductOption { ID = 3, ImagePaths = "images/product/201d53a8-1efs-41ef-aac4-aadsff8414cd/book.jpg", OptionLabel1 = "Format", Options1 = "E-book,Paperback,Hardcover" });

            //Product
            modelBuilder.Entity<Commodity>().HasData(new Commodity { ID = 1, Name = "Cup", Description = "Cup", Rating = 2.0, Price = 4.00, UserID = "201d53a8-1efs-41ef-aac4-aadsff8414cd", ProductTypeID = 1, Units = 4, ProductOptionID = 1});
            modelBuilder.Entity<Commodity>().HasData(new Commodity { ID = 2, Name = "Redbull", Description = "Energy Drink", Rating = 4.0, Price = 2.50, UserID = "201d53a8-1efs-41ef-aac4-aadsff8414cd", ProductTypeID = 2, NumberReviews = 1, Units = 10, ProductOptionID = 2 });
            modelBuilder.Entity<Service>().HasData(new Service { ID = 3, Name = "Consulting Detective", Description = "Solves crimes", ImagePath = "images/sherlock.jpg", Rating = 5.0, Price = 20.00, UserID = "6f942aca-57a7-4adb-a6b5-5d9796cdf10b", ProductTypeID = 4, NumberReviews = 1});
            modelBuilder.Entity<Commodity>().HasData(new Commodity { ID = 4, Name = "Book", Description = "New book!", Rating = 4.0, Price = 9.00, UserID = "201d53a8-1efs-41ef-aac4-aadsff8414cd", ProductTypeID = 3, RentAvailable = true, ProductOptionID = 3, Units=20});

            //ProductRentals
            modelBuilder.Entity<ProductRentals>().HasData(new ProductRentals { ProductID = 4, UserID = "201d53a8-1fe6-41ef-aac4-aad4af8414cd", ExpirationDate = new DateTime(2021, 10, 3), ID = 1 });

            //ProductHistories
            modelBuilder.Entity<ProductHistory>().HasData(new ProductHistory { ProductID = 2, UserID = "201d53a8-1fe6-41ef-aac4-aad4af8414cd", Date = new DateTime(2021, 1, 10), Type = "sale", ID = 1 });
            modelBuilder.Entity<ProductHistory>().HasData(new ProductHistory { ProductID = 3, UserID = "201d53a8-1fe6-41ef-aac4-aad4af8414cd", Date = new DateTime(2021, 1, 10), Type = "service", ID = 2 });

            //Review
            modelBuilder.Entity<Review>().HasData(new Review { ID = 6, ProductID = 2, Content = "Liked the taste", Rating = 4.0, Upvotes = 0, NumComments = 0, UserID = "201d53a8-1fe6-41ef-aac4-aad4af8414cd", DateTime = DateTime.Now });
            modelBuilder.Entity<Review>().HasData(new Review { ID = 7, ProductID = 3, Content = "Solved an impossible crime!", Rating = 5.0, Upvotes = 0, NumComments = 0, UserID = "201d53a8-1fe6-41ef-aac4-aad4af8414cd", DateTime = DateTime.Now });

            //Users
            modelBuilder.Entity<User>().HasData(new User
            {
                ID = "6f942aca-57a7-4adb-a6b5-5d9796cdf10b",
                UserName = "sherlocked",
                FirstName = "Sherlock",
                LastName = "Holmes",
                DOB = new DateTime(1976, 5, 15),
                Rating = 0,
                BloodType = "B+",
                ImagePath = "images/sherlock.jpg",
                AccountTypeID = 4
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                ID = "201d53a8-1fe6-41ef-aac4-aad4af8414cd",
                UserName = "yn05",
                FirstName = "Yohan",
                LastName = "Ninan",
                DOB = new DateTime(1998, 10, 15),
                Rating = 0,
                BloodType = "A+",
                ImagePath = "images/yohan.jpg",
                AccountTypeID = 3
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                ID = "201d53a8-1fe6-41ef-aac4-aadsff8414cd",
                UserName = "mk44",
                FirstName = "Mikasa",
                LastName = "Ackerman",
                DOB = new DateTime(2001, 2, 10),
                Rating = 0,
                BloodType = "O+",
                ImagePath = "images/mikasa.jpg",
                AccountTypeID = 1
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                ID = "201d53a8-1efs-41ef-aac4-aadsff8414cd",
                UserName = "miles4",
                FirstName = "Miles",
                LastName = "Morales",
                DOB = new DateTime(1987, 4, 13),
                Rating = 0,
                BloodType = "B+",
                ImagePath = "images/miles.jpg",
                AccountTypeID = 2
            });


            // UserTimeSlots
            modelBuilder.Entity<UserTimeslots>().HasData(new UserTimeslots { ID = 1, EarliestDate = DateTime.Now, Available = true, Repitition = "weekly", TimeslotID = 10, UserID = "6f942aca-57a7-4adb-a6b5-5d9796cdf10b", EndDate = DateTime.Now.AddDays(5)});
            modelBuilder.Entity<UserTimeslots>().HasData(new UserTimeslots { ID = 2, EarliestDate = DateTime.Now, Available = true, Repitition = "monthly", TimeslotID = 34, UserID = "6f942aca-57a7-4adb-a6b5-5d9796cdf10b" , EndDate = DateTime.Now.AddMonths(1)});


            //Stories
            Story story1 = new Story { ID = 1, Title = "Test 1", Content = "Test Content", Upvotes = 1, UserID = "201d53a8-1fe6-41ef-aac4-aad4af8414cd", DateTime = new DateTime(2020, 11, 8), SpaceID = 1, NumComments = 1 };
            Story story2 = new Story { ID = 2, Title = "Test 2", Content = "F1 race this week! Grazzie Ragazzi", Upvotes = 2, UserID = "6f942aca-57a7-4adb-a6b5-5d9796cdf10b", DateTime = new DateTime(2020, 11, 8), SpaceID = 2, NumComments = 1 };
            modelBuilder.Entity<Story>().HasData(story1);
            modelBuilder.Entity<Story>().HasData(story2);

            //Comments
            Comment comment1 = new Comment { ID = 3, Upvotes = 0, UserID = "6f942aca-57a7-4adb-a6b5-5d9796cdf10b", Content = "what's a test", ParentContentID = 1, NumComments = 1 };
            Comment comment2 = new Comment { ID = 4, Upvotes = 2, UserID = "201d53a8-1fe6-41ef-aac4-aad4af8414cd", Content = "Oh please O_O", ParentContentID = 3 };
            Comment comment3 = new Comment { ID = 5, Upvotes = 1, UserID = "201d53a8-1fe6-41ef-aac4-aad4af8414cd", Content = "- _ -", ParentContentID = 2 };

            modelBuilder.Entity<Comment>().HasData(comment1);
            modelBuilder.Entity<Comment>().HasData(comment2);
            modelBuilder.Entity<Comment>().HasData(comment3);

            //UserTextReactions
            modelBuilder.Entity<UserTextReactions>().HasData(new UserTextReactions { ID = 1, Liked = true, TextContentID = 1, UserID = "201d53a8-1fe6-41ef-aac4-aadsff8414cd" });
            modelBuilder.Entity<UserTextReactions>().HasData(new UserTextReactions { ID = 2, Liked = false, Commented=true, TextContentID = 1, UserID = "6f942aca-57a7-4adb-a6b5-5d9796cdf10b"});
            modelBuilder.Entity<UserTextReactions>().HasData(new UserTextReactions { ID = 3, Liked = true, Commented=true, TextContentID = 3, UserID = "201d53a8-1fe6-41ef-aac4-aad4af8414cd" });
            modelBuilder.Entity<UserTextReactions>().HasData(new UserTextReactions { ID = 4, Liked = false, Commented = true, TextContentID = 2, UserID = "201d53a8-1fe6-41ef-aac4-aad4af8414cd" });

            //Space
            modelBuilder.Entity<Space>().HasData(new Space { ID = 1, Name = "N/A", NumberStories = 1});
            modelBuilder.Entity<Space>().HasData(new Space { ID = 2, Name = "Formula 1", NumberStories = 1 });
        }
    }
}
