using System;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
public static void Run(Stream myBlob, string name, TraceWriter log)
{
    log.Info($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
    
    List<Student> NewStudents = new List<Student>();
    List<Student> CommitStudents = new List<Student>();
    
    int studentCount = 0;
    int commitCount = 100;
    int currCount = 0;
    int totalCount = 0;    

    log.Info("----- Reading from CSV -----");
    using(var reader = new StreamReader(myBlob)){    
        while (!reader.EndOfStream){
            var line = reader.ReadLine();
            var values = line.Split(',');                        
            int studentId = Convert.ToInt32(values[2]);
            Student s = new Student(studentId,values[0],values[1]);                
            NewStudents.Add(s);            
            studentCount ++;   
        }
        log.Info($"----- Students Read: {studentCount} -----"); 
    }

    // commit in chucnks to avoid db bottleneck
    foreach (Student s in NewStudents){

        currCount++;
        totalCount++;
        CommitStudents.Add(s);

        if(currCount == commitCount || totalCount == NewStudents.Count){
            using (var context = new StudentContext()){
                log.Info($"----- Saving to SQL: Started {DateTime.Now} -----");
                context.Students.AddRange(CommitStudents);
                context.SaveChanges();
                log.Info($"----- Saving to SQL: Completed {DateTime.Now} -----");
            }
            CommitStudents.Clear();
            currCount = 0;
        }
    }

}

[Table("Student")]
public class Student
{
    public Student(int asustudentid, string firstname, string lastname)
    {
        this.StudentId = Guid.NewGuid();
        this.ASUStudentId = asustudentid;
        this.FirstName = firstname;
        this.LastName = lastname;
        this.DateCreated = DateTime.Now;
        this.DateModified = DateTime.Now;
    }
    public Guid StudentId { get; set; }
    public int ASUStudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string APIUrl { get; set; }
    public string APIKey { get; set; }
    public string APIResponse { get; set; }
    public bool APIIsValid { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}

public class StudentContext : DbContext
{
    public StudentContext()
        : base("name=DefaultConnection")
    {
    }

    public virtual DbSet<Student> Students { get; set; }
}