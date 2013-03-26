using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Reflection.Emit;
using System.Reflection;
using System.Diagnostics;
using AllenCopeland.Abstraction.Slf.Compilers;

[assembly: InternalsVisibleTo("StructuralTypingExample<Builder>")]
static class _
{
    internal static readonly AssemblyBuilder projectBuilder = CreateProjectBuilder();
    internal static readonly ModuleBuilder rootBuilderModule = CreateRootModule(projectBuilder);

    private static ModuleBuilder CreateRootModule(AssemblyBuilder projectBuilder)
    {
        return projectBuilder.DefineDynamicModule(projectBuilder.GetName().Name);
    }

    private static AssemblyBuilder CreateProjectBuilder()
    {
        string name = "StructuralTypingExample<Builder>";
        var assemblyName = new AssemblyName(name);
        return AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
    }
}

namespace StructuralTypingExample
{

    internal class StructuralTypingExample
    {
        internal const string _DatabaseGuid_ = "96C4D785-53E9-4E75-8FF8-37E9814BDD5B";
        internal const string _DatasetGuid_ = "7C905BAF-B6A6-4EA7-A666-9EFF67210C2B";
        private const byte byteTimes = 20;
        [HasStructuralTypeBridge(_DatabaseGuid_, typeof(_StructuralExample1_TestDatabase1))]
        internal class TestDatabase1 : SimpleDatabase<TestDataset1> { }
        [HasStructuralTypeBridge(_DatabaseGuid_, typeof(_StructuralExample1_TestDatabase2))]
        internal class TestDatabase2 : SimpleDatabase<TestDataset2> { }

        [HasStructuralTypeBridge(_DatasetGuid_, typeof(_StructuralExample1_TestDataset1))]
        internal class TestDataset1 : SimpleDataset<TestDatabase1, TestDataset1, byte>
        {
            public TestDataset1(TestDatabase1 db, byte[] data)
                : base(db, data) { }


            protected override TestDataset1 OnClone(TestDatabase1 db, byte[] data)
            {
                return new TestDataset1(db, data);
            }
        }
        [HasStructuralTypeBridge(_DatasetGuid_, typeof(_StructuralExample1_TestDataset2))]
        internal class TestDataset2 : SimpleDataset<TestDatabase2, TestDataset2, byte>
        {
            public TestDataset2(TestDatabase2 db, byte[] data)
                : base(db, data) { }

            protected override TestDataset2 OnClone(TestDatabase2 db, byte[] data)
            {
                return new TestDataset2(db, data);
            }
        }

        private static void StructuralExample<[HasStructuralTypeBehavior(typeof(_StructuralExample_Cache<,>._StructuralExample_Bridge_For__TDataset__), _DatasetGuid_)] TDataset, [HasStructuralTypeBehavior(typeof(_StructuralExample_Cache<,>._StructuralExample_Bridge_For__TDatabase__), _DatabaseGuid_)]TDatabase>()
            where TDatabase :
                new()//, has
        /*  {
                TDataset AddDataset(string name, TDataset data);
                TDataset this[string name] { get; }
            }
        where TDataset :
            has 
            {
                TDataset(TDatabase uInst, byte[] data);
                TDatabase Database { get; }
                byte this[int index] { get; }
                byte[] GetDataCopy();
                TDataset Clone();
            }*/
        {
            var bridge = _StructuralExample_Cache<TDataset, TDatabase>.GetBridges();
            var testDatabase = new TDatabase();
            var datasetBridge = bridge.Item1.Value;
            var databaseBridge = bridge.Item2.Value;
            for (int i = 0; i < byteTimes; i++)
            {
                var dataInside = new byte[i + 1];
                dataInside[0] = (byte)(i + 1);
                var inst = datasetBridge.ctor(testDatabase, dataInside);
                var dataset = databaseBridge.AddDataset(testDatabase, string.Format("Dataset{0}", i + 1), inst);
                var cloneSet = datasetBridge.Clone(dataset);
                var cloneData = datasetBridge.GetDataCopy(cloneSet);
                var elementsEqual = datasetBridge.get_Item(dataset, 0) == cloneData[0];
                var databasesEqual = datasetBridge.get_Database(dataset).Equals(datasetBridge.get_Database(cloneSet));
            }
        }

        protected static void Main()
        {
            Func<dynamic> databaseFactory1 = () => new TestDatabase1();
            Func<dynamic> databaseFactory2 = () => new TestDatabase2();
            Func<dynamic> databaseFactory3 = () => new DatabaseAlt1();

            Func<dynamic, dynamic, dynamic> datasetFactory1 = (a, b) => new TestDataset1(a, b);
            Func<dynamic, dynamic, dynamic> datasetFactory2 = (a, b) => new TestDataset2(a, b);
            Func<dynamic, dynamic, dynamic> datasetFactory3 = (a, b) => new DatasetAlt1(a, b);

            StructuralExample<TestDataset1, TestDatabase1>();
            StructuralExample<TestDataset2, TestDatabase2>();
            StructuralExample<DatasetAlt1, DatabaseAlt1>();
            DirectExample1();
            DirectExample2();
            DirectExample3();
            DynamicExample(databaseFactory1, datasetFactory1);
            DynamicExample(databaseFactory2, datasetFactory2);
            DynamicExample(databaseFactory3, datasetFactory3);

            const int times = 5000;
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            for (int i = 0; i < times; i++)
            {
                StructuralExample<TestDataset1, TestDatabase1>();
                StructuralExample<TestDataset2, TestDatabase2>();
                StructuralExample<DatasetAlt1, DatabaseAlt1>();
            }
            sw.Stop();
            var structural = sw.Elapsed;
            sw.Restart();
            for (int i = 0; i < times; i++)
            {
                DirectExample1();
                DirectExample2();
                DirectExample3();
            }
            sw.Stop();
            var direct = sw.Elapsed;
            sw.Restart();
            for (int i = 0; i < times; i++)
            {
                DynamicExample(databaseFactory1, datasetFactory1);
                DynamicExample(databaseFactory2, datasetFactory2);
                DynamicExample(databaseFactory3, datasetFactory3);
            }
            sw.Stop();
            var dynamic = sw.Elapsed;

            Console.WriteLine("Structural Elapsed time: {0}", structural);
            Console.WriteLine("Direct Elapsed time: {0}", direct);
            Console.WriteLine("Dynamic Elapsed time: {0}", dynamic);

        }

        private static void DirectExample1()
        {
            var database = new TestDatabase1();
            for (int i = 0; i < byteTimes; i++)
            {
                var dataInside = new byte[i + 1];
                dataInside[0] = (byte)(i + 1);
                var inst = new TestDataset1(database, dataInside);
                var dataset = database.AddDataset(string.Format("Dataset{0}", i + 1), inst);
                var cloneSet = dataset.Clone();
                var cloneData = cloneSet.GetDataCopy();
                var elementsEqual = dataset[0] == cloneData[0];
                var databasesEqual = dataset.Database.Equals(cloneSet.Database);
            }
        }

        private static void DirectExample2()
        {
            var database = new TestDatabase2();
            for (int i = 0; i < byteTimes; i++)
            {
                var dataInside = new byte[i + 1];
                dataInside[0] = (byte)(i + 1);
                var inst = new TestDataset2(database, dataInside);
                var dataset = database.AddDataset(string.Format("Dataset{0}", i + 1), inst);
                var cloneSet = dataset.Clone();
                var cloneData = cloneSet.GetDataCopy();
                var elementsEqual = dataset[0] == cloneData[0];
                var databasesEqual = dataset.Database.Equals(cloneSet.Database);
            }
        }

        private static void DirectExample3()
        {
            var database = new DatabaseAlt1();
            for (int i = 0; i < byteTimes; i++)
            {
                var dataInside = new byte[i + 1];
                dataInside[0] = (byte)(i + 1);
                var inst = new DatasetAlt1(database, dataInside);
                var dataset = database.AddDataset(string.Format("Dataset{0}", i + 1), inst);
                var cloneSet = dataset.Clone();
                var cloneData = cloneSet.GetDataCopy();
                var elementsEqual = dataset[0] == cloneData[0];
                var databasesEqual = dataset.Database.Equals(cloneSet.Database);
            }
        }

        private static void DynamicExample(Func<dynamic> databaseFactory, Func<dynamic, dynamic, dynamic> datasetFactory)
        {
            var database = databaseFactory();
            for (int i = 0; i < byteTimes; i++)
            {
                var dataInside = new byte[i + 1];
                dataInside[0] = (byte)(i + 1);
                var inst = datasetFactory(database, dataInside);
                var dataset = database.AddDataset(string.Format("Dataset{0}", i + 1), inst);
                var cloneSet = dataset.Clone();
                var cloneData = cloneSet.GetDataCopy();
                var elementsEqual = dataset[0] == cloneData[0];
                var databasesEqual = dataset.Database.Equals(cloneSet.Database);
            }
        }
        [CompilerGenerated()]
        public class _StructuralExample1_TestDatabase1 :
            _StructuralExample_Cache<TestDataset1, TestDatabase1>._StructuralExample_Bridge_For__TDatabase__
        {
            public TestDataset1 get_Item(TestDatabase1 targetInst, string datasetName)
            {
                return targetInst[datasetName];
            }

            public TestDataset1 AddDataset(TestDatabase1 targetInst, string datasetName, TestDataset1 data)
            {
                return targetInst.AddDataset(datasetName, data);
            }
        }
        [CompilerGenerated()]
        public class _StructuralExample1_TestDataset1 :
            _StructuralExample_Cache<TestDataset1, TestDatabase1>._StructuralExample_Bridge_For__TDataset__
        {

            public TestDataset1 ctor(TestDatabase1 uInst, byte[] data)
            {
                return new TestDataset1(uInst, data);
            }

            public TestDatabase1 get_Database(TestDataset1 targetInst)
            {
                return targetInst.Database;
            }

            public byte get_Item(TestDataset1 targetInst, int index)
            {
                return targetInst[index];
            }

            public byte[] GetDataCopy(TestDataset1 targetInst)
            {
                return targetInst.GetDataCopy();
            }

            public TestDataset1 Clone(TestDataset1 targetInst)
            {
                return targetInst.Clone();
            }
        }

        [CompilerGenerated()]
        public class _StructuralExample1_TestDatabase2 :
            _StructuralExample_Cache<TestDataset2, TestDatabase2>._StructuralExample_Bridge_For__TDatabase__
        {
            public TestDataset2 get_Item(TestDatabase2 targetInst, string datasetName)
            {
                return targetInst[datasetName];
            }

            public TestDataset2 AddDataset(TestDatabase2 targetInst, string datasetName, TestDataset2 data)
            {
                return targetInst.AddDataset(datasetName, data);
            }
        }
        [CompilerGenerated()]
        public class _StructuralExample1_TestDataset2 :
            _StructuralExample_Cache<TestDataset2, TestDatabase2>._StructuralExample_Bridge_For__TDataset__
        {

            public TestDataset2 ctor(TestDatabase2 uInst, byte[] data)
            {
                return new TestDataset2(uInst, data);
            }

            public TestDatabase2 get_Database(TestDataset2 targetInst)
            {
                return targetInst.Database;
            }

            public byte get_Item(TestDataset2 targetInst, int index)
            {
                return targetInst[index];
            }

            public byte[] GetDataCopy(TestDataset2 targetInst)
            {
                return targetInst.GetDataCopy();
            }

            public TestDataset2 Clone(TestDataset2 targetInst)
            {
                return targetInst.Clone();
            }
        }

        [CompilerGenerated()]
        public class _StructuralExample1_DatabaseAlt1 :
            _StructuralExample_Cache<DatasetAlt1, DatabaseAlt1>._StructuralExample_Bridge_For__TDatabase__
        {
            public DatasetAlt1 get_Item(DatabaseAlt1 targetInst, string datasetName)
            {
                return targetInst[datasetName];
            }

            public DatasetAlt1 AddDataset(DatabaseAlt1 targetInst, string datasetName, DatasetAlt1 data)
            {
                return targetInst.AddDataset(datasetName, data);
            }
        }

        [CompilerGenerated()]
        public class _StructuralExample1_DatasetAlt1 :
            _StructuralExample_Cache<DatasetAlt1, DatabaseAlt1>._StructuralExample_Bridge_For__TDataset__
        {

            public DatasetAlt1 ctor(DatabaseAlt1 uInst, byte[] data)
            {
                return new DatasetAlt1(uInst, data);
            }

            public DatabaseAlt1 get_Database(DatasetAlt1 targetInst)
            {
                return targetInst.Database;
            }

            public byte get_Item(DatasetAlt1 targetInst, int index)
            {
                return targetInst[index];
            }

            public byte[] GetDataCopy(DatasetAlt1 targetInst)
            {
                return targetInst.GetDataCopy();
            }

            public DatasetAlt1 Clone(DatasetAlt1 targetInst)
            {
                return targetInst.Clone();
            }
        }

        public static class _StructuralExample_Cache<TDataset, TDatabase>
            where TDatabase :
                new()
        {
            private static readonly Guid __DatabaseGuid_ = Guid.Parse(_DatabaseGuid_);
            private static readonly Guid __DatasetGuid_ = Guid.Parse(_DatasetGuid_);
            private static Tuple<Lazy<_StructuralExample_Bridge_For__TDataset__>, Lazy<_StructuralExample_Bridge_For__TDatabase__>> bridges;
            private static Tuple<bool, bool> hasExternalBridges = new Tuple<bool, bool>(false, false);
            private static int dynamicBridgeID = 0;
            public interface _StructuralExample_Bridge_For__TDataset__
            {
                /* *
                 * The structure of the item as it would be generated.
                 * *
                 * get_ members would be replaced with an indexed
                 * property that accepts an instance of the target
                 * type.
                 * *
                 * Constructors are used to instantiate members of the
                 * target type when you need something other than a 
                 * parameterless constructor.
                 * */
                TDataset ctor(TDatabase uInst, byte[] data);
                TDatabase get_Database(TDataset targetInst);
                byte get_Item(TDataset targetInst, int index);
                byte[] GetDataCopy(TDataset targetInst);
                TDataset Clone(TDataset targetInst);
            }

            public interface _StructuralExample_Bridge_For__TDatabase__
            {
                TDataset get_Item(TDatabase targetInst, string datasetName);
                TDataset AddDataset(TDatabase targetInst, string datasetName, TDataset data);
            }

            public static _StructuralExample_Bridge_For__TDataset__ CreateTDatasetBridge()
            {
                var tBridge = typeof(_StructuralExample_Bridge_For__TDataset__);
                Type tTarget = typeof(TDataset);
                var prefab = StandardCompilerAids.GetInternalBridge<_StructuralExample_Bridge_For__TDataset__>(tTarget, tBridge, __DatasetGuid_);
                if (prefab != null)
                    return prefab;
                //if (tBridge.IsAssignableFrom(tTarget)) ;
                /* *
                 * Collect the target type's members
                 * that we're going to be focusing on.
                 * */

                var targetCtorUData = tTarget.GetConstructor(BindingFlags.ExactBinding | BindingFlags.Instance | BindingFlags.Public, Type.DefaultBinder, new Type[] { typeof(TDatabase), typeof(byte[]) }, null);
                var targetUInstProp = tTarget.GetProperty("Database", BindingFlags.Public | BindingFlags.Instance, Type.DefaultBinder, typeof(TDatabase), new Type[0], null);
                var targetUInstGet = targetUInstProp.GetGetMethod(false);
                var targetItemProp = tTarget.GetProperty("Item", BindingFlags.Public | BindingFlags.Instance, Type.DefaultBinder, typeof(byte), new Type[] { typeof(int) }, null);
                var targetItemGet = targetItemProp.GetGetMethod(false);
                var targetGetDataCopy = tTarget.GetMethod("GetDataCopy", BindingFlags.Instance | BindingFlags.Public | BindingFlags.ExactBinding, Type.DefaultBinder, new Type[0], null);
                var targetClone = tTarget.GetMethod("Clone", BindingFlags.Instance | BindingFlags.ExactBinding | BindingFlags.Public, Type.DefaultBinder, new Type[0], null);

                /* *
                 * Collect the bridge members which will
                 * ultimately be used to connect to the overrides.
                 * *
                 * This way it knows which signatures connect to
                 * the interface.
                 * */
                var bridgeCtor = tBridge.GetMethod("ctor", BindingFlags.Instance | BindingFlags.ExactBinding | BindingFlags.Public, Type.DefaultBinder, new Type[] { typeof(TDatabase), typeof(byte[]) }, null);
                var bridgeUInst = tBridge.GetMethod("get_Database", BindingFlags.Public | BindingFlags.Instance | BindingFlags.ExactBinding, Type.DefaultBinder, new Type[] { typeof(TDataset) }, null);
                var bridgeItem = tBridge.GetMethod("get_Item", BindingFlags.Public | BindingFlags.Instance | BindingFlags.ExactBinding, Type.DefaultBinder, new Type[] { typeof(TDataset), typeof(int) }, null);
                var bridgeGetDataCopy = tBridge.GetMethod("GetDataCopy", BindingFlags.Instance | BindingFlags.Public | BindingFlags.ExactBinding, Type.DefaultBinder, new Type[] { typeof(TDataset) }, null);
                var bridgeClone = tBridge.GetMethod("Clone", BindingFlags.Instance | BindingFlags.Public | BindingFlags.ExactBinding, Type.DefaultBinder, new Type[] { typeof(TDataset) }, null);

                /* *
                 * Build the dynamic type associated to the
                 * current bridge.
                 * */
                var resultedBridge = _.rootBuilderModule.DefineType(string.Format("<{0}, {1}>Bridge{1}<StructuralExample<TDataset>>", tTarget.FullName, typeof(TDatabase).FullName, ++dynamicBridgeID), TypeAttributes.AnsiClass | TypeAttributes.NotPublic | TypeAttributes.Sealed);
                resultedBridge.AddInterfaceImplementation(tBridge);

                var ctor = resultedBridge.DefineMethod("<.ctor>", MethodAttributes.PrivateScope | MethodAttributes.Virtual, CallingConventions.HasThis, typeof(TDataset), new Type[] { typeof(TDatabase), typeof(byte[]) });
                resultedBridge.DefineMethodOverride(ctor, bridgeCtor);

                var ctorILGen = ctor.GetILGenerator();
                ctorILGen.Emit(OpCodes.Ldarg_1);
                ctorILGen.Emit(OpCodes.Ldarg_2);
                ctorILGen.Emit(OpCodes.Newobj, targetCtorUData);
                ctorILGen.Emit(OpCodes.Ret);

                var get_UInst = resultedBridge.DefineMethod("<<get>Database>", MethodAttributes.PrivateScope | MethodAttributes.Virtual, CallingConventions.HasThis, typeof(TDatabase), new Type[] { typeof(TDataset) });
                resultedBridge.DefineMethodOverride(get_UInst, bridgeUInst);
                var get_UInstILGen = get_UInst.GetILGenerator();
                get_UInstILGen.Emit(OpCodes.Ldarg_1);
                get_UInstILGen.Emit(OpCodes.Callvirt, targetUInstGet);
                get_UInstILGen.Emit(OpCodes.Ret);

                var get_Item = resultedBridge.DefineMethod("<<get>Item>", MethodAttributes.PrivateScope | MethodAttributes.Virtual, CallingConventions.HasThis, typeof(byte), new Type[] { typeof(TDataset), typeof(int) });
                resultedBridge.DefineMethodOverride(get_Item, bridgeItem);
                var get_ItemILGen = get_Item.GetILGenerator();
                get_ItemILGen.Emit(OpCodes.Ldarg_1);
                get_ItemILGen.Emit(OpCodes.Ldarg_2);
                get_ItemILGen.Emit(OpCodes.Callvirt, targetItemGet);
                get_ItemILGen.Emit(OpCodes.Ret);

                var dataCopy = resultedBridge.DefineMethod("<GetDataCopy>", MethodAttributes.PrivateScope | MethodAttributes.Virtual, CallingConventions.HasThis, typeof(byte[]), new Type[] { typeof(TDataset) });
                resultedBridge.DefineMethodOverride(dataCopy, bridgeGetDataCopy);
                var dataCopyILGen = dataCopy.GetILGenerator();
                dataCopyILGen.Emit(OpCodes.Ldarg_1);
                dataCopyILGen.Emit(OpCodes.Callvirt, targetGetDataCopy);
                dataCopyILGen.Emit(OpCodes.Ret);

                var clone = resultedBridge.DefineMethod("<Clone>", MethodAttributes.PrivateScope | MethodAttributes.Virtual, CallingConventions.HasThis, typeof(TDataset), new Type[] { typeof(TDataset) });
                resultedBridge.DefineMethodOverride(clone, bridgeClone);
                var cloneILGen = clone.GetILGenerator();
                cloneILGen.Emit(OpCodes.Ldarg_1);
                cloneILGen.Emit(OpCodes.Callvirt, targetClone);
                cloneILGen.Emit(OpCodes.Ret);

                return (_StructuralExample_Bridge_For__TDataset__)Activator.CreateInstance(resultedBridge.CreateType());
            }

            public static _StructuralExample_Bridge_For__TDatabase__ CreateTDatabaseBridge()
            {
                var tBridge = typeof(_StructuralExample_Bridge_For__TDatabase__);
                var tTarget = typeof(TDatabase);

                var prefab = StandardCompilerAids.GetInternalBridge<_StructuralExample_Bridge_For__TDatabase__>(tTarget, tBridge, __DatabaseGuid_);
                if (prefab != null)
                    return prefab;

                var targetItem = tTarget.GetProperty("Item", BindingFlags.Public | BindingFlags.Instance | BindingFlags.ExactBinding, Type.DefaultBinder, typeof(TDataset), new Type[] { typeof(string) }, null);
                var targetItemGet = targetItem.GetGetMethod(false);
                var targetAddDataset = tTarget.GetMethod("AddDataset", BindingFlags.ExactBinding | BindingFlags.Public | BindingFlags.Instance, Type.DefaultBinder, new Type[] { typeof(string), typeof(TDataset) }, null);

                var bridgeItem = tBridge.GetMethod("get_Item", BindingFlags.Public | BindingFlags.Instance | BindingFlags.ExactBinding, Type.DefaultBinder, new Type[] { typeof(TDatabase), typeof(string) }, null);
                var bridgeAddDataSet = tBridge.GetMethod("AddDataset", BindingFlags.ExactBinding | BindingFlags.Public | BindingFlags.Instance, Type.DefaultBinder, new Type[] { typeof(TDatabase), typeof(string), typeof(TDataset) }, null);

                var resultedBridge = _.rootBuilderModule.DefineType(string.Format("<{0}, {1}>Bridge{1}<StructuralExample<TDatabase>>", typeof(TDataset).FullName, tTarget.FullName, ++dynamicBridgeID), TypeAttributes.AnsiClass | TypeAttributes.NotPublic | TypeAttributes.Sealed);
                resultedBridge.AddInterfaceImplementation(tBridge);

                var getItem = resultedBridge.DefineMethod("<get<Item>>", MethodAttributes.PrivateScope | MethodAttributes.Virtual, CallingConventions.HasThis, typeof(TDataset), new Type[] { typeof(TDatabase), typeof(string) });
                resultedBridge.DefineMethodOverride(getItem, bridgeItem);
                var getItemILGen = getItem.GetILGenerator();
                getItemILGen.Emit(OpCodes.Ldarg_1);
                getItemILGen.Emit(OpCodes.Ldarg_2);
                getItemILGen.Emit(OpCodes.Callvirt, targetItemGet);
                getItemILGen.Emit(OpCodes.Ret);

                var addDataSet = resultedBridge.DefineMethod("<AddDataset>", MethodAttributes.PrivateScope | MethodAttributes.Virtual, CallingConventions.HasThis, typeof(TDataset), new Type[] { typeof(TDatabase), typeof(string), typeof(TDataset) });
                resultedBridge.DefineMethodOverride(addDataSet, bridgeAddDataSet);
                var addDataSetILGen = addDataSet.GetILGenerator();
                addDataSetILGen.Emit(OpCodes.Ldarg_1);
                addDataSetILGen.Emit(OpCodes.Ldarg_2);
                addDataSetILGen.Emit(OpCodes.Ldarg_3);
                addDataSetILGen.Emit(OpCodes.Callvirt, targetAddDataset);
                addDataSetILGen.Emit(OpCodes.Ret);

                return (_StructuralExample_Bridge_For__TDatabase__)Activator.CreateInstance(resultedBridge.CreateType());

            }

            public static Tuple<Lazy<_StructuralExample_Cache<TDataset, TDatabase>._StructuralExample_Bridge_For__TDataset__>, Lazy<_StructuralExample_Cache<TDataset, TDatabase>._StructuralExample_Bridge_For__TDatabase__>> GetBridges()
            {
                return bridges ?? (bridges = new Tuple<Lazy<_StructuralExample_Bridge_For__TDataset__>, Lazy<_StructuralExample_Bridge_For__TDatabase__>>(new Lazy<_StructuralExample_Bridge_For__TDataset__>(CreateTDatasetBridge), new Lazy<_StructuralExample_Bridge_For__TDatabase__>(CreateTDatabaseBridge)));
            }

            internal static void SetBridges(_StructuralExample_Bridge_For__TDatabase__ databaseBridge, _StructuralExample_Bridge_For__TDataset__ datasetBridge)
            {
                bridges = new Tuple<Lazy<_StructuralExample_Bridge_For__TDataset__>, Lazy<_StructuralExample_Bridge_For__TDatabase__>>(new Lazy<_StructuralExample_Bridge_For__TDataset__>(new Func<_StructuralExample_Bridge_For__TDataset__>(() => datasetBridge)), new Lazy<_StructuralExample_Bridge_For__TDatabase__>(new Func<_StructuralExample_Bridge_For__TDatabase__>(() => databaseBridge)));
            }
        }

        #region Test StructuralExample types
        public abstract class SimpleDataset<TDatabase, TDataset, TData>
            where TDataset :
                SimpleDataset<TDatabase, TDataset, TData>
        {
            private TDatabase db;
            private TData[] data;

            public SimpleDataset(TDatabase db, TData[] data)
            {
                this.db = db;
                this.data = data;
            }

            public TData this[int index]
            {
                get
                {
                    return this.data[index];
                }
            }

            public TData[] GetDataCopy()
            {
                return (TData[])this.data.Clone();
            }

            public TDatabase Database
            {
                get
                {
                    return this.db;
                }
            }

            public TDataset Clone()
            {
                return this.OnClone(this.db, this.data);
            }

            protected abstract TDataset OnClone(TDatabase db, TData[] data);
        }

        public class SimpleDatabase<TDataset>
        {
            private Dictionary<string, TDataset> dbCopy = new Dictionary<string, TDataset>();

            public TDataset this[string name]
            {
                get
                {
                    return dbCopy[name];
                }
            }

            public TDataset AddDataset(string name, TDataset dataset)
            {
                this.dbCopy.Add(name, dataset);
                return this[name];
            }
        }


        [HasStructuralTypeBridge(_DatasetGuid_, typeof(_StructuralExample1_DatasetAlt1))]
        public class DatasetAlt1
        {
            private DatabaseAlt1 db;
            private byte[] data;

            public DatasetAlt1(DatabaseAlt1 db, byte[] data)
            {
                this.db = db;
                this.data = data;
            }

            public byte this[int index]
            {
                get
                {
                    return this.data[index];
                }
            }

            public byte[] GetDataCopy()
            {
                return (byte[])this.data.Clone();
            }

            public DatabaseAlt1 Database
            {
                get
                {
                    return this.db;
                }
            }

            public DatasetAlt1 Clone()
            {
                return new DatasetAlt1(this.db, this.GetDataCopy());
            }
        }

        [HasStructuralTypeBridge(_DatabaseGuid_, typeof(_StructuralExample1_DatabaseAlt1))]
        public class DatabaseAlt1
        {
            private Dictionary<string, DatasetAlt1> dbCopy = new Dictionary<string, DatasetAlt1>();

            public DatasetAlt1 this[string name]
            {
                get
                {
                    return dbCopy[name];
                }
            }

            public DatasetAlt1 AddDataset(string name, DatasetAlt1 dataset)
            {
                this.dbCopy.Add(name, dataset);
                return this[name];
            }
        }

        #endregion
    }
}
