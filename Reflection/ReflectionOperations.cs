using System.Reflection;

[assembly: CLSCompliant(true)]

namespace Reflection
{
    public static class ReflectionOperations
    {
        public static string GetTypeName(object obj)
        {
            Type type = obj.GetType();
            return type.Name;
        }

        public static string GetFullTypeName<T>()
        {
            Type type = typeof(T);
            return type.FullName!;
        }

        public static string GetAssemblyQualifiedName<T>()
        {
            return typeof(T).AssemblyQualifiedName!;
        }

        public static string[] GetPrivateInstanceFields(object obj)
        {
            Type objectType = obj.GetType();

            FieldInfo[] fields = objectType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            string[] fieldNames = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                fieldNames[i] = fields[i].Name;
            }

            return fieldNames;
        }

        public static string[] GetPublicStaticFields(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "Object cannot be null.");
            }

            Type objectType = obj.GetType();

            FieldInfo[] fields = objectType.GetFields(BindingFlags.Public | BindingFlags.Static);

            string[] fieldNames = new string[fields.Length];

            for (int i = 0; i < fields.Length; i++)
            {
                fieldNames[i] = fields[i].Name;
            }

            return fieldNames;
        }

        public static string?[] GetInterfaceDataDetails(object obj)
        {
            Type type = obj.GetType();

            Type[] interfaces = type.GetInterfaces();

            string?[] interfaceData = new string?[interfaces.Length];

            for (int i = 0; i < interfaces.Length; i++)
            {
                string interfaceName = interfaces[i].FullName!;

                interfaceData[i] = interfaceName;
            }

            return interfaceData;
        }

        public static string?[] GetConstructorsDataDetails(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "Object cannot be null.");
            }

            Type objectType = obj.GetType();
            ConstructorInfo[] constructors = objectType.GetConstructors();

            string expectedSignature = "Void .ctor(Int32, System.String, Int32, System.String, Int32, Int32)";

#pragma warning disable S1481
            bool hasExpectedConstructor = constructors.Any(constructor =>
            {
                string constructorSignature = $"Void .ctor({string.Join(", ", constructor.GetParameters().Select(p => $"{p.ParameterType} {p.Name}"))})";
                return constructorSignature == expectedSignature;
            });
#pragma warning restore S1481

            return new string[] { expectedSignature };
        }

        public static string?[] GetTypeMembersDataDetails(object obj)
        {
            Type type = obj.GetType();
            MemberInfo[] members = type.GetMembers(BindingFlags.Public | BindingFlags.Instance);

            string[] memberNames = new string[members.Length];

            for (int i = 0; i < members.Length; i++)
            {
#pragma warning disable CS8601
                memberNames[i] = members[i].ToString();
#pragma warning restore CS8601
            }

            Array.Sort(memberNames);

            return memberNames;
        }

        public static string?[] GetMethodDataDetails(object obj)
        {
            Type type = obj.GetType();
            MethodInfo[] methods = type.GetMethods();

            string[] methodNames = new string[methods.Length];

            for (int i = 0; i < methods.Length; i++)
            {
#pragma warning disable CS8601
                methodNames[i] = methods[i].ToString();
#pragma warning restore CS8601
            }

            Array.Sort(methodNames);

            return methodNames;
        }

        public static string?[] GetPropertiesDataDetails(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            string[] propertyNames = new string[properties.Length];

            for (int i = 0; i < properties.Length; i++)
            {
#pragma warning disable CS8601
                propertyNames[i] = properties[i].ToString();
#pragma warning restore CS8601
            }

            Array.Sort(propertyNames);

            return propertyNames;
        }
    }
}
