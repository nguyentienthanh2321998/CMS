using Microsoft.VisualBasic;
using System;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.Common;
using System.Runtime.Intrinsics.X86;

namespace FSH.WebApi.Application.Catalog.Brands;

public class CreateBrandRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class CreateBrandRequestValidator : CustomValidator<CreateBrandRequest>
{
    public CreateBrandRequestValidator(IReadRepository<Brand> repository, IStringLocalizer<CreateBrandRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new BrandByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Brand {0} already Exists.", name]);
}

public class CreateBrandRequestHandler : IRequestHandler<CreateBrandRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Brand> _repository;
    private readonly IDbConnection _dbConnection;

    public CreateBrandRequestHandler(IRepositoryWithEvents<Brand> repository,
        IDbConnection dbConnection)
    {
        _repository = repository;
        _dbConnection = dbConnection;
    }

    public async Task<Guid> Handle(CreateBrandRequest request, CancellationToken cancellationToken)
    {
        {
            // Check for valid DbConnection object.
            if (_dbConnection != null)
            {
                using (_dbConnection)
                {
                    _dbConnection.Open();
                
                    var tran = _dbConnection.BeginTransaction();
                    try
                    {
                        // Open the connection.
                      
                        // Create and execute the DbCommand.
                        var command = _dbConnection.CreateCommand();
                       
                        command.CommandType = CommandType.Text;

                        command.CommandText =
                            "INSERT INTO catalog.brands (name , description) VALUES ('Low Carb','Low Carb')";
                        var rows = command.ExecuteNonQuery();

                       
                        
                           
                        

                        // Display number of rows inserted.
                        Console.WriteLine("Inserted {0} rows.", rows);
                        tran.Commit();
                    }
                    // Handle data errors.
                    catch (DbException exDb)
                    {
                        tran.Rollback();
                        Console.WriteLine("DbException.GetType: {0}", exDb.GetType());
                        Console.WriteLine("DbException.Source: {0}", exDb.Source);
                        Console.WriteLine("DbException.ErrorCode: {0}", exDb.ErrorCode);
                        Console.WriteLine("DbException.Message: {0}", exDb.Message);
                    }
                    // Handle all other exceptions.
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        Console.WriteLine("Exception.Message: {0}", ex.Message);
                        
                    }
                    _dbConnection.Close();
                }
            }
            else
            {
                Console.WriteLine("Failed: DbConnection is null.");
            }
        }

        try
        {
            // Do work here.  
        }
        catch (DbException ex)
        {
            // Display information about the exception.  
            Console.WriteLine("GetType: {0}", ex.GetType());
            Console.WriteLine("Source: {0}", ex.Source);
            Console.WriteLine("ErrorCode: {0}", ex.ErrorCode);
            Console.WriteLine("Message: {0}", ex.Message);
        }
        finally
        {
            // Perform cleanup here.  
        }
        var brand = new Brand(request.Name, request.Description);
        await _repository.AddAsync(brand, cancellationToken);

        return brand.Id;
    }
}