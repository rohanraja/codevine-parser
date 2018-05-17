using System;
using System.Collections.Generic;
using CodeRecordHelpers;
using Newtonsoft.Json;


namespace TestCSharpProject
{
    public class ClassA
    {
		public ClassB clsB; 
		public int count = 0;
		public double flt = 3.1423243;
		public bool tstBool = false;
		public string ccccc = "test";
		public List<int> intLst = new List<int>() ;
		public List<ClassB> clsBLst = new List<ClassB>();

		public ClassA()
        {
			OnConstructorEnter();

			Guid MrID = OnMethodEnter("ClassA");
            
			DeclareClassField("TestCSharpProject.ClassB", "clsB", this.GetHashCode(), isRef: true, isSourceAvailable: true);
			UpdateVariableRef("clsB", this.GetHashCode(), MrID, clsB);
            
			DeclareClassField("int", "count", this.GetHashCode(), false);
			UpdateVariableValue("count", this.GetHashCode(), MrID, count);

			DeclareClassField("List<int>", "intLst", this.GetHashCode(), false);
            UpdateVariableValue("intLst", this.GetHashCode(), MrID, intLst);

			DeclareClassField("List<ClassB>", "clsBLst", this.GetHashCode(), false);
			UpdateVariableValue("clsBLst", this.GetHashCode(), MrID, clsBLst);
            
			Console.WriteLine(intLst.GetHashCode());
			intLst.Add(23);
			intLst.Add(4);
			Console.WriteLine(JsonConvert.SerializeObject(intLst));

			clsB = new ClassB("Hello from ClassA");
			UpdateVariableRef("clsB", this.GetHashCode(), MrID, clsB);

			Console.WriteLine(JsonConvert.SerializeObject(clsBLst));
			clsBLst.Add(clsB);
			Console.WriteLine(JsonConvert.SerializeObject(clsBLst));
			//Console.WriteLine(JsonConvert.SerializeObject(clsB));
			Console.WriteLine("STR= " + JsonConvert.SerializeObject(this));
			Console.WriteLine(JsonConvert.SerializeObject(count));
			Console.WriteLine(JsonConvert.SerializeObject(ccccc));
			Console.WriteLine(JsonConvert.SerializeObject(tstBool));
			Console.WriteLine(JsonConvert.SerializeObject(flt));


			count = 22;
			UpdateVariableValue("count", this.GetHashCode(), MrID, count);
        }


		public void MethodA_1()
        {
			Guid MrID = OnMethodEnter("MethodA_1");

			count = 99;
			UpdateVariableValue("count", this.GetHashCode(), MrID, count);
			clsB.MethodB_1();
        }

        public int GetCount()
		{
			return count;
		}

        //// *************** ////
		///  Record Helpers ///
       
        Guid OnMethodEnter(string methodName)
		{
			Guid mrid = Guid.NewGuid();
			MethodHelpers.Instance().OnMethodEnter(mrid, "ClassA.cs", methodName);
			return mrid;
		}

		private void DeclareClassField(string fieldType, string fieldName, int clrInstanceId, bool isRef, bool isSourceAvailable = false)
        {
        }

        int GetCLRInstanceID(object target)
		{
            if(target == null)
    			return 0;
			int hashCode = target.GetHashCode();
			return hashCode;

		}
		private void UpdateVariableValue(string identifier, int clrInstanceId, Guid mrID, object rawValue)
        {
        }

		private void UpdateVariableRef(string identifier, int clrInstanceId, Guid mrID, object objRef)
        {
			UpdateVariableValue(identifier, clrInstanceId, mrID, GetCLRInstanceID(objRef));

        }
		private void LogLineRun(int lineNo, Guid mrid)
        {
        }

		private void OnConstructorEnter()
        {
        }

    }
}
