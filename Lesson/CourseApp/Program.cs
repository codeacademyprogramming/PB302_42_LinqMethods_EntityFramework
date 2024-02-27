

using CourseApp;
using CourseApp.Models;
using Microsoft.EntityFrameworkCore;

string opt;
do
{
    Console.WriteLine("1. Add group");
    Console.WriteLine("2. Get group by id");
    Console.WriteLine("3. Get all groups");
    Console.WriteLine("4. Add student");
    Console.WriteLine("5. Get all students");


    opt = Console.ReadLine();

    CourseDbContext context = new CourseDbContext();

    switch (opt)
    {
        case "1":
            Console.Write("No:");
            string no = Console.ReadLine();
            Console.Write("Limit:");
            int limit = Convert.ToInt32(Console.ReadLine());
            Console.Write("StartDate:");
            DateTime startDate = Convert.ToDateTime(Console.ReadLine());
            var newGroup = new Group
            {
                No = no,
                Limit = limit,
                StartDate = startDate,
            };

            context.Groups.Add(newGroup);
            context.SaveChanges();
            break;
        case "2":
            Console.Write("Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var group = context.Groups.Find(id);

            Console.WriteLine(group.Id+"-"+group.No+"-"+group.Limit+"-"+group.StartDate.ToString("yyyy-MM-dd"));
            break;
        case "3":
            var groups = context.Groups.Include(x=>x.Students).ToList();

            foreach (var item in groups)
            {
                Console.WriteLine(item.Id+"-"+item.No+"-"+item.Limit+"-"+item.Students.Count);
            }
            break;
        case "4":
            Console.Write("Fullname: ");
            string fullname = Console.ReadLine();
            Console.Write("Point: ");
            byte point = Convert.ToByte(Console.ReadLine());
            
            Console.WriteLine("al groups:");
            foreach (var item in context.Groups.ToList())
                Console.WriteLine(item.Id + "-" + item.No);

            Console.Write("GroupId: ");
            int groupId = Convert.ToInt32(Console.ReadLine());

            Student newStd = new Student
            {
                Fullname = fullname,
                GroupId = groupId,
                Point = point
            };

            context.Students.Add(newStd);
            context.SaveChanges();

            break;
        case "5":
            var students = context.Students.Include(x=>x.Group).ToList();

            foreach (var std in students)
            {
                Console.WriteLine(std.Id+"-"+std.Fullname+"-"+std.Point+"-"+std.Group.No);
            }
            break;
        case "0":
            break;
        default:
            break;
    }
} while (opt!="0");






void practice()
{
    Group group1 = new Group
    {
        No = "PB302",
        Limit = 10,
        StartDate = new DateTime(2023, 12, 11)
    };
    Group group2 = new Group
    {
        No = "PB303",
        Limit = 10,
        StartDate = new DateTime(2023, 12, 11)
    };

    List<Group> groups = new List<Group>() { group1, group2 };

    //Insert a data into table
    //using (CourseDbContext context = new CourseDbContext())
    //{
    //    context.Groups.Add(group1);
    //    context.SaveChanges();
    //}

    //Insert many data into table
    using (CourseDbContext context = new CourseDbContext())
    {
        context.Groups.AddRange(groups);
        context.SaveChanges();
    }

    //get a data from table
    using (CourseDbContext context = new CourseDbContext())
    {
        var data = context.Groups.First();
        data = context.Groups.FirstOrDefault();
        data = context.Groups.Single();
        data = context.Groups.SingleOrDefault();
        data = context.Groups.Last();
        data = context.Groups.LastOrDefault();
        data = context.Groups.Find(1);

        var list = context.Groups.Skip(5).Take(10);
        list = list.SkipWhile(x => x.Limit == 0).Take(5);
        list = list.TakeWhile(x => x.Limit == 0);

        var groupedList = context.Groups.GroupBy(x => x.Limit).ToList();

        var selectedList = context.Groups.Select(x => new GroupDisplay
        {
            No = x.No,
            Date = x.StartDate.ToString("dd-MM-yyyy"),
        }).ToList();

    }


    //get all groups 
    using (CourseDbContext context = new CourseDbContext())
    {
        var data = context.Groups.Where(x => x.Limit > 5);
        data = data.Where(x => x.No.Contains("P"));
        data = data.OrderBy(x => x.StartDate);

        var result = data.AsNoTracking().ToList();
    }

    //update a group
    using (CourseDbContext context = new CourseDbContext())
    {
        //var data = context.Groups.FirstOrDefault(x => x.Id == 1);
        var data = context.Groups.Find(1);

        data.Limit = 15;

        context.SaveChanges();
    }

    //delete a group
    using (CourseDbContext context = new CourseDbContext())
    {
        //var data = context.Groups.FirstOrDefault(x => x.Id == 1);
        var data = context.Groups.Find(1);

        context.Groups.Remove(data);

        context.SaveChanges();
    }

    //delete many groups
    using (CourseDbContext context = new CourseDbContext())
    {
        //var data = context.Groups.FirstOrDefault(x => x.Id == 1);
        var data = context.Groups.Where(x => x.Limit < 5).ToList();

        context.Groups.RemoveRange(data);

        context.SaveChanges();
    }



}

