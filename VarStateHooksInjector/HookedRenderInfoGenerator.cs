using System;
using System.Collections.Generic;
using VarStateHooksInjector.Entities;

namespace VarStateHooksInjector
{
	public class HookedRenderInfoGenerator
	{
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
			string methodStr = HookTemplates.MethodEnterHook(classInfo.RelativeFilePath, methodInfo.Name);
			if (!info.renderingInfo.ContainsKey(0))
				info.renderingInfo[0] = new List<string>() { };
			info.renderingInfo[0].Add(methodStr);

            // Add LineExecHooks
			foreach(int blockid in methodInfo.blockInfo.Keys)
			{
				var statInfos = methodInfo.blockInfo[blockid].StatementInfos;
				for (int i = 0; i < statInfos.Count; i++)
				{
					var statInfo = statInfos[i];
					string likeHook = HookTemplates.LineExecHook(statInfo.LineNo, "");
					info.renderingInfo[blockid].Add(likeHook);
					info.renderingInfo[blockid].Add(i.ToString());
				}
			}

			return info;
		}

		public CodeRunBlockRenderingInfo CodeRunBlockRenderInfoForConstructor(ClassInfo classInfo, int id)
		{
			return CodeRunBlockRenderInfoForMethod(classInfo, id);
		}
	}
}