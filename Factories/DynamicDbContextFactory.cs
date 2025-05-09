namespace AspnetCoreStarter.Factories;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

public static class DynamicDbContextFactory
{
  /// <summary>
  /// Generates a DbContext instance dynamically at runtime based on provided entity types and connection string.
  /// </summary>
  /// <param name="connectionString">The database connection string.</param>
  /// <param name="entityTypes">A collection of types representing the entities to include in the DbContext.</param>
  /// <returns>An instance of a dynamically generated DbContext.</returns>
  /// <exception cref="ArgumentNullException">Thrown if connectionString or entityTypes is null.</exception>
  /// <exception cref="InvalidOperationException">Thrown if no entity types are provided.</exception>
  public static DbContext CreateDbContext(string connectionString, IEnumerable<Type> entityTypes)
  {
    if (connectionString == null)
    {
      throw new ArgumentNullException(nameof(connectionString));
    }

    if (entityTypes == null)
    {
      throw new ArgumentNullException(nameof(entityTypes));
    }

    if (!entityTypes.Any())
    {
      throw new InvalidOperationException("At least one entity type must be provided.");
    }

    // Generate a unique name for the dynamic DbContext type
    string dbContextTypeName = $"DynamicDbContext_{Guid.NewGuid().ToString().Replace("-", "")}";

    // Create a new dynamic assembly and module
    var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("DynamicDbContextAssembly"), AssemblyBuilderAccess.Run);
    var moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicDbContextModule");

    // Define the DbContext base class
    var dbContextTypeInfo = typeof(DbContext).GetTypeInfo();

    // Define the new DbContext type
    var typeBuilder = moduleBuilder.DefineType(dbContextTypeName,
                                              TypeAttributes.Public | TypeAttributes.Class,
                                              dbContextTypeInfo.AsType());

    // Define the constructor that accepts DbContextOptions
    var constructorBuilder = typeBuilder.DefineConstructor(
        MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName,
        CallingConventions.Standard,
        new[] { typeof(DbContextOptions) });

    // Generate constructor IL to call the base DbContext constructor
    var constructorIL = constructorBuilder.GetILGenerator();
    constructorIL.Emit(System.Reflection.Emit.OpCodes.Ldarg_0); // Load 'this'
    constructorIL.Emit(System.Reflection.Emit.OpCodes.Ldarg_1); // Load the DbContextOptions argument
    constructorIL.Emit(System.Reflection.Emit.OpCodes.Call, dbContextTypeInfo.DeclaredConstructors.First()); // Call base constructor
    constructorIL.Emit(System.Reflection.Emit.OpCodes.Ret); // Return

    // Define DbSet properties for each entity type
    foreach (var entityType in entityTypes)
    {
      // Create the DbSet<TEntity> generic type
      var dbSetGenericType = typeof(DbSet<>).MakeGenericType(entityType);

      // Define the DbSet property
      var propertyBuilder = typeBuilder.DefineProperty(entityType.Name + "s",
                                                      // PropertyAttributes.Public | PropertyAttributes.Virtual,
                                                      PropertyAttributes.None,
                                                      dbSetGenericType,
                                                      null);

      // Define the getter method
      var getterBuilder = typeBuilder.DefineMethod($"get_{entityType.Name}s",
                                                  MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
                                                  dbSetGenericType,
                                                  Type.EmptyTypes);

      var getterIL = getterBuilder.GetILGenerator();
      getterIL.Emit(System.Reflection.Emit.OpCodes.Ldarg_0); // Load 'this'
      getterIL.Emit(System.Reflection.Emit.OpCodes.Ldstr, entityType.Name + "s"); // Load the property name
      getterIL.Emit(System.Reflection.Emit.OpCodes.Call, typeof(DbContext).GetMethod("Set", Type.EmptyTypes).MakeGenericMethod(entityType)); // Call the Set<TEntity>() method
      getterIL.Emit(System.Reflection.Emit.OpCodes.Ret); // Return

      propertyBuilder.SetGetMethod(getterBuilder);
    }

    // Override the OnConfiguring method
    var onConfiguringMethodBuilder = typeBuilder.DefineMethod(
        "OnConfiguring",
        MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig,
        CallingConventions.Standard, dbContextTypeInfo.AsType(),
        new[] { typeof(DbContextOptionsBuilder) });

    var onConfiguringIL = onConfiguringMethodBuilder.GetILGenerator();
    onConfiguringIL.Emit(System.Reflection.Emit.OpCodes.Ldarg_1); // Load the DbContextOptionsBuilder argument
    onConfiguringIL.Emit(System.Reflection.Emit.OpCodes.Ldstr, connectionString); // Load the connection string
                                                                                  // You might need to adjust the specific database provider method here (e.g., UseSqlServer, UseNpgsql, UseSqlite)
                                                                                  // This example assumes you want to use a generic database provider that might not exist directly.
                                                                                  // In a real-world scenario, you would likely have a configuration or know the provider type.
                                                                                  // For a basic example, let's assume a method called "UseDatabase" exists (you'd replace this).
                                                                                  // **Important:** Replace the following line with the actual method call for your desired database provider.
                                                                                  // For example, for SQL Server:
                                                                                  // onConfiguringIL.Emit(System.Reflection.Emit.OpCodes.Call, typeof(SqlServerDbContextOptionsExtensions).GetMethod("UseSqlServer", new[] { typeof(DbContextOptionsBuilder), typeof(string) }));
                                                                                  // For SQLite:
                                                                                  // onConfiguringIL.Emit(System.Reflection.Emit.OpCodes.Call, typeof(SqliteDbContextOptionsBuilderExtensions).GetMethod("UseSqlite", new[] { typeof(DbContextOptionsBuilder), typeof(string) }));
                                                                                  // For a more generic approach (requires a base class or interface with a configuration method):
                                                                                  // onConfiguringIL.Emit(System.Reflection.Emit.OpCodes.Call, typeof(YourDatabaseConfiguration).GetMethod("ConfigureDatabase", new[] { typeof(DbContextOptionsBuilder), typeof(string) }));
                                                                                  //
                                                                                  // For this dynamic example, we'll use a placeholder. You MUST replace this with your actual provider configuration.
    var useDatabaseMethod = typeof(DbContextOptionsBuilder).GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .FirstOrDefault(m => m.Name == "UseSqlServer" && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == typeof(string));

    if (useDatabaseMethod != null)
    {
      onConfiguringIL.Emit(System.Reflection.Emit.OpCodes.Call, useDatabaseMethod);
    }
    else
    {
      // Handle the case where the specific UseDatabase method is not found.
      // You might want to throw an exception or log a warning.
      Console.WriteLine("Warning: Could not find a suitable UseDatabase method on DbContextOptionsBuilder. Ensure your database provider package is installed and the correct method name is used.");
    }
    onConfiguringIL.Emit(System.Reflection.Emit.OpCodes.Ret); // Return

    // Create the dynamic type
    var dynamicDbContextType = typeBuilder.CreateTypeInfo().AsType();

    // Create an instance of DbContextOptionsBuilder
    var optionsBuilderType = typeof(DbContextOptionsBuilder);
    var optionsBuilder = Activator.CreateInstance(optionsBuilderType);

    // Invoke the OnConfiguring method on the options builder
    var onConfiguringMethod = dynamicDbContextType.GetMethod("OnConfiguring", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
    onConfiguringMethod?.Invoke(Activator.CreateInstance(dynamicDbContextType, new object[] { optionsBuilder }), new object[] { optionsBuilder });

    // Get the generic DbContextOptions<TContext> type
    var dbContextOptionsGenericType = typeof(DbContextOptions<>).MakeGenericType(dynamicDbContextType);

    // Find the constructor of DbContextOptions that takes a DbContextOptionsBuilder
    var dbContextOptionsConstructor = dbContextOptionsGenericType.GetConstructor(new[] { optionsBuilderType });

    if (dbContextOptionsConstructor == null)
    {
      throw new InvalidOperationException($"Could not find a suitable constructor for {dbContextOptionsGenericType.Name}.");
    }

    // Create an instance of DbContextOptions
    var dbContextOptions = dbContextOptionsConstructor.Invoke(new[] { optionsBuilder });

    // Create an instance of the dynamic DbContext
    var dbContextInstance = (DbContext)Activator.CreateInstance(dynamicDbContextType, new object[] { dbContextOptions });

    return dbContextInstance;
  }
}

//// Example Usage:
//public class Customer
//{
//  public int Id { get; set; }
//  public string Name { get; set; }
//}

//public class Order
//{
//  public int Id { get; set; }
//  public DateTime OrderDate { get; set; }
//  public int CustomerId { get; set; }
//  public Customer Customer { get; set; }
//}

//public class Program
//{
//  public static void Main(string[] args)
//  {
//    string connectionString = "YourConnectionStringHere"; // Replace with your actual connection string
//    List<Type> entityTypes = new List<Type> { typeof(Customer), typeof(Order) };

//    try
//    {
//      using (var dbContext = DynamicDbContextFactory.CreateDbContext(connectionString, entityTypes))
//      {
//        // Now you can work with the dynamically created DbContext
//        Console.WriteLine($"Dynamically created DbContext: {dbContext.GetType().Name}");

//        // Access the DbSet properties
//        var customers = dbContext.Set<Customer>();
//        var orders = dbContext.Set<Order>();

//        // Perform database operations as needed
//        // ...
//      }
//    }
//    catch (Exception ex)
//    {
//      Console.WriteLine($"An error occurred: {ex.Message}");
//    }
//  }
//}
