using VRFEngine.Data;
using VRFEngine.Model;
using VRFEngine.Repository.Implementation;
using VRFEngine.Repository.Interface;
using VRFEngine.Test.Helper;
using Microsoft.AspNetCore.Http;
using System;
using Xunit;
using System.Collections.Generic;

namespace VRFEngine.Test.Repository
{
    public class IntegrationTest : IDisposable
    {
        private CRUDRepository _crudRepository;
        private DataContext _dataContext;

        public IntegrationTest()
        {
            _dataContext = DataContextHelper.GetDataContext();
            IHttpContextAccessor httpContextAccessor = MockHelperFactory.GetHttpContextWithUser().Object;
            ILoggerService loggerService = MockHelperFactory.GetLoggerService().Object;
            _crudRepository = new CRUDRepository(_dataContext, loggerService, httpContextAccessor);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataContext.Dispose();
                _dataContext = null;
                _crudRepository = null;
            }
        }

        [Fact]
        public void Test()
        {
            // Setup

            // Create Form
            Form form = EntityFactory.GetForm();
            FormVersion formVersion = EntityFactory.GetFormVersion();
            formVersion.Version = 1;
            formVersion.Form = form;

            // Create Field
            Field firstNameField = EntityFactory.GetField();
            firstNameField.Type = FieldType.Text;
            FieldVersion firstNameFieldVersion = EntityFactory.GetFieldVersion();
            firstNameFieldVersion.Version = 1;
            firstNameFieldVersion.Name = "FirstName";
            firstNameFieldVersion.Field = firstNameField;

            Field lastNameField = EntityFactory.GetField();
            lastNameField.Type = FieldType.Text;
            FieldVersion lastNameFieldVersion = EntityFactory.GetFieldVersion();
            lastNameFieldVersion.Version = 1;
            lastNameFieldVersion.Name = "LastName";
            lastNameFieldVersion.Field = lastNameField;
            
            formVersion.Fields = new List<FieldVersion>();
            formVersion.Fields.Add(firstNameFieldVersion);
            formVersion.Fields.Add(lastNameFieldVersion);

            // Test
            Form result = _crudRepository.Create(form);

            // Assert
            Assert.NotNull(result);
        }
    }
}
