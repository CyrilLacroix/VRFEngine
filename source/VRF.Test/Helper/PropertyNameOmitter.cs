using AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VRFEngine.Test.Helper
{
    internal class PropertyNameOmitter : ISpecimenBuilder
    {
        private readonly IEnumerable<string> names;

        internal PropertyNameOmitter(params string[] names)
        {
            this.names = names;
        }

        public object Create(object request, ISpecimenContext context)
        {
            PropertyInfo propInfo = request as PropertyInfo;
            if (propInfo != null && names.Contains(propInfo.Name))
            {
                return new OmitSpecimen();
            }

            return new NoSpecimen();
        }
    }
}
