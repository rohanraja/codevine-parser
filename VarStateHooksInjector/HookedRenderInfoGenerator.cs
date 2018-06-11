using System;
using System.Collections.Generic;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjector
{
	public class HookedRenderInfoGenerator
	{
		public Dictionary<string, bool> config = new Dictionary<string, bool>() { };

		public CodeRunBlockRenderingInfo CodeRunBlockRenderInfoForMethod(ClassInfo classInfo, int id)
		{
			var info = new CodeRunBlockRenderingInfo();
			var methodInfo = classInfo.GetCodeRunnerInfo(id);

            // Initialize render block dictionaries
			foreach (int blockid in methodInfo.blockInfo.Keys)
			{
				info.renderingInfo[blockid] = new List<string>() { };
			}

			// Add OnMethodHook
			if(shouldHookOnMethod())
			{
				if (!info.renderingInfo.ContainsKey(0))
                    info.renderingInfo[0] = new List<string>() { };

    			string methodStr = HookTemplates.MethodEnterHook(classInfo.RelativeFilePath, methodInfo.Name);
    			info.renderingInfo[0].Add(methodStr);
			}


            // Add Field Initial Value Update Hooks
			if(methodInfo.IsConstructor && shouldHookFieldInits())
            {
				if (!info.renderingInfo.ContainsKey(0))
                    info.renderingInfo[0] = new List<string>() { };

                foreach(var finfo in classInfo.FieldInfos.Values)
                {
					foreach (var fname in finfo.Names)
					{
						string hookexpr = HookTemplates.FieldUpdateHook(fname, classInfo.Name, FieldGenerator.GetPrefixedName(fname));
                        info.renderingInfo[0].Add(hookexpr);
					}
                }
                
            }


            // Add LineExecHooks
			foreach(int blockid in methodInfo.blockInfo.Keys)
			{
				var statInfos = methodInfo.blockInfo[blockid].StatementInfos;
				for (int i = 0; i < statInfos.Count; i++)
				{
					var statInfo = statInfos[i];

					if(shouldHookLineExec())
					{
    					string likeHook = HookTemplates.LineExecHook(statInfo.LineNo, "");
    					info.renderingInfo[blockid].Add(likeHook);
					}

					info.renderingInfo[blockid].Add(i.ToString());
				}

                // Add line hook for close brace of the block
				int closeLineNo = methodInfo.blockInfo[blockid].CloseBraceLineNo;
				if(closeLineNo != -1 && shouldHookLineExec())
				{
					string closeBraceStr = HookTemplates.LineExecHook(closeLineNo, "");
                    info.renderingInfo[blockid].Add(closeBraceStr);
				}
			}

			return info;
		}

		private bool shouldHookLineExec()
		{
			return true;
		}

		private bool shouldHookFieldInits()
		{
			return true;
		}

		private bool shouldHookOnMethod()
		{
			return true;
		}

		public CodeRunBlockRenderingInfo CodeRunBlockRenderInfoForConstructor(ClassInfo classInfo, int id)
		{
			return CodeRunBlockRenderInfoForMethod(classInfo, id);
		}
	}
}