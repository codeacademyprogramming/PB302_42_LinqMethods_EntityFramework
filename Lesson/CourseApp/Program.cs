

using CourseApp;
using CourseApp.Models;
using Microsoft.EntityFrameworkCore;



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

List<Group> groups = new List<Group>() { group1,group2 };

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
using(CourseDbContext context = new CourseDbContext())
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


