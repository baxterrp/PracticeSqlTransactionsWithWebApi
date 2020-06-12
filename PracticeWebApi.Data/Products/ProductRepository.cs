using Dapper;
using PracticeWebApi.CommonClasses.Exceptions;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeWebApi.Data.Products
{
    public class ProductRepository : IProductRepository
    {
        private string _connectionString = "Data Source = Silver; Initial Catalog = PracticeCommerce; Integrated Security = True;";
        private readonly string _updateActivationStatus = 
            @"UPDATE Products Set [IsActive] = @IsActive WHERE [Id] = @Id";
        private readonly string _insertNewProduct =
            @"INSERT INTO Products (id, name, description, price, groupId, isActive)" +
            @" VALUES (@Id, @Name, @Description, @Price, @GroupId, @IsActive)";
        private readonly string _findProductById = @"SELECT * FROM Products WHERE [Id] = @Id";
        private readonly string _findProductByGroupId = @"SELECT * FROM Products WHERE [GroupId] = @GroupId AND [IsActive] = 1";
        private readonly string _updateProduct =
            @"UPDATE Products SET name = @Name, " +
            @"description = @Description, " +
            @"price = @Price," +
            @"groupId = @GroupId WHERE [Id] = @Id";

        public async Task ActivateProduct(string productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(_updateActivationStatus, new { Id = productId, IsActive = 1 });
            }
        }

        public async Task DeactiveProduct(string productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(_updateActivationStatus, new { Id = productId, IsActive = 0 });
            }
        }

        public async Task<ProductDataEntity> AddProduct(ProductDataEntity product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(_insertNewProduct, product);

                return product;
            }
        }

        public async Task<ProductDataEntity> FindProductById(string productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<ProductDataEntity>(_findProductById, new { Id = productId });

                if (!result?.Any() ?? false) throw new ResourceNotFoundException($"No resource found with Id {productId}");
                if (result.Count() > 1) throw new DuplicateResourceException($"Multiple resources found with Id {productId}");

                return result.Single();
            }
        }

        public async Task<IList<ProductDataEntity>> GetProductsByGroupId(string groupId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var results = await connection.QueryAsync<ProductDataEntity>(_findProductByGroupId, new { GroupId = groupId });

                return results.ToList();
            }
        }

        public async Task UpdateProduct(ProductDataEntity product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(_updateProduct, product);
            }
        }
    }
}
