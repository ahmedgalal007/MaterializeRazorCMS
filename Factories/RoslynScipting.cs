//// If not used remove EntityFrameWorkCore.InMemory Package

//using Microsoft.CodeAnalysis.CSharp.Scripting;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Reflection;
//using System.Threading.Tasks;

//namespace AspnetCoreStarter.Factories;
//public class DynamicDbContextGenerator
//{
//  public static async Task<Type> GenerateDynamicEntityAndDbContext(string entityName, string tableName, params (string propName, Type propType)[] properties)
//  {
//    // 1. Generate the Entity class C# code
//    string entityCode = $"public class {entityName} {{ ";
//    foreach (var prop in properties)
//    {
//      entityCode += $"public {prop.propType.Name} {prop.propName} {{ get; set; }} ";
//    }
//    entityCode += "}";

//    // 2. Generate the DbContext class C# code
//    string dbContextCode = $@"
//            using Microsoft.EntityFrameworkCore;
//            using System; // For Guid or other types if needed

//            public class MyDynamicDbContext : DbContext
//            {{
//                public DbSet<{entityName}> {entityName}s {{ get; set; }}

//                protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//                {{
//                    // This is just an example, you'd inject or configure this properly
//                    optionsBuilder.UseInMemoryDatabase(""{entityName}Db""); 
//                }}

//                protected override void OnModelCreating(ModelBuilder modelBuilder)
//                {{
//                    modelBuilder.Entity<{entityName}>().ToTable(""{tableName}"");
//                    // Add more configurations based on your 'properties' if needed
//                }}
//            }}
//        ";

//    string fullCode = entityCode + dbContextCode;

//    // 3. Compile the code using Roslyn
//    var script = CSharpScript.Create(fullCode,
//        options: ScriptOptions.Default
//            .AddReferences(typeof(object).Assembly)
//            .AddReferences(typeof(System.Collections.Generic.List<>).Assembly)
//            .AddReferences(typeof(Microsoft.EntityFrameworkCore.DbContext).Assembly)
//            .AddReferences(typeof(Microsoft.EntityFrameworkCore.InMemoryDbContextOptionsExtensions).Assembly) // For UseInMemoryDatabase
//                                                                                                              // Add any other assemblies your dynamic code might need
//        );

//    var compilation = script.GetCompilation();
//    var stream = new System.IO.MemoryStream();
//    var emitResult = compilation.Emit(stream);

//    if (!emitResult.Success)
//    {
//      foreach (var diagnostic in emitResult.Diagnostics)
//      {
//        Console.WriteLine(diagnostic);
//      }
//      throw new InvalidOperationException("Compilation failed.");
//    }

//    stream.Seek(0, System.IO.SeekOrigin.Begin);
//    Assembly dynamicAssembly = Assembly.Load(stream.ToArray());

//    // 4. Find and return the DbContext type
//    Type dbContextType = dynamicAssembly.GetType("MyDynamicDbContext");
//    if (dbContextType == null)
//    {
//      throw new InvalidOperationException("Could not find MyDynamicDbContext type.");
//    }

//    return dbContextType;
//  }

//  public static async Task Main(string[] args)
//  {
//    // Example usage:
//    Type dynamicDbContextType = await GenerateDynamicEntityAndDbContext(
//        "Product",
//        "Products",
//        ("Id", typeof(int)),
//        ("Name", typeof(string)),
//        ("Price", typeof(decimal))
//    );

//    Console.WriteLine($"Generated DbContext Type: {dynamicDbContextType.FullName}");

//    // Now you can create an instance of this DbContext and use it
//    using (dynamic dbContext = Activator.CreateInstance(dynamicDbContextType))
//    {
//      // You can now interact with the dynamically created DbSet
//      // Example: Add a product (requires casting to specific DbSet or using reflection)
//      // This part becomes more complex as you need to interact dynamically
//      // For a simple example, you might try to cast if you know the entity type structure,
//      // or use reflection to call methods like Add, SaveChanges etc.

//      // Example of adding an entity using reflection (more general)
//      var entityType = dbContext.GetType().Assembly.GetType("Product");
//      var newEntity = Activator.CreateInstance(entityType);
//      entityType.GetProperty("Id").SetValue(newEntity, 1);
//      entityType.GetProperty("Name").SetValue(newEntity, "Dynamic Widget");
//      entityType.GetProperty("Price").SetValue(newEntity, 9.99m);

//      var dbSetProperty = dbContext.GetType().GetProperty("Products"); // Assuming DbSet property name is Product<entityName>s
//      if (dbSetProperty != null)
//      {
//        var dbSet = dbSetProperty.GetValue(dbContext);
//        var addMethod = dbSet.GetType().GetMethod("Add"); // MethodInfo for DbSet<T>.Add
//        addMethod.Invoke(dbSet, new object[] { newEntity });
//      }
//      else
//      {
//        Console.WriteLine("DbSet 'Products' not found via reflection.");
//      }

//      // Note: SaveChanges needs to be called on the DbContext instance
//      MethodInfo saveChangesMethod = dynamicDbContextType.GetMethod("SaveChanges");
//      if (saveChangesMethod != null)
//      {
//        int changes = (int)saveChangesMethod.Invoke(dbContext, null);
//        Console.WriteLine($"Changes saved: {changes}");
//      }
//      else
//      {
//        Console.WriteLine("SaveChanges method not found.");
//      }

//      // Example of querying using reflection
//      if (dbSetProperty != null)
//      {
//        var dbSet = dbSetProperty.GetValue(dbContext);
//        // ToListAsync or ToList would involve more reflection.
//        // For simplicity, let's just assume we added something and it's there.
//        Console.WriteLine("Added Dynamic Widget.");
//      }
//    }
//  }
//}
