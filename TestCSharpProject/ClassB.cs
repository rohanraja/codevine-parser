using System;
using CodeRecordHelpers;

namespace TestCSharpProject
{
    public class ClassB
    {
		public string name = "";

		public int CodeVineInstanceID = 0;


        public ClassB(string pName)
        {
			OnConstructorEnter();
			var MrID = OnMethodEnter("ClassB");

            DeclareClassField("System.String", "name", this.GetHashCode(), true);
            UpdateVariableRef("name", this.GetHashCode(), MrID, name);

			name = pName;
        }


		public void MethodB_1()
		{
			name = name + " MODIFIED";

		}

		Guid OnMethodEnter(string methodName)
        {
            Guid mrid = Guid.NewGuid();
            CodeHooks.Instance().OnMethodEnter(mrid, "TestCSharpProject.ClassB", methodName);
            return mrid;
        }

        private void DeclareClassField(string fieldType, string fieldName, int clrInstanceId, bool isRef)
        {
        }

        int GetCLRInstanceID(object target)
        {
            if (target == null)
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
			CodeVineInstanceID = this.GetHashCode();
		}

    }
}
