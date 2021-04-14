using System.Collections.Generic;
using UnityEngine;
using Pixyz.OptimizeSDK.Native;
using Pixyz.Commons.Utilities;
using Pixyz.OptimizeSDK.Native.Geom;
using Pixyz.Commons.UI.Editor;

namespace Pixyz.Toolbox.Editor
{
    public class SaveAction : PixyzFunction
    {
        public override int id => 57113405;
        public override int order => 40;
        public override string menuPathRuleEngine => "Misc/Save as .pxz";
        public override string menuPathToolbox => "Misc/Save as .pxz";
        public override string tooltip => "Save a .pxz";

        [UserParameter]
        public FilePath outputPath = new FilePath("", true);

        public override bool preProcess(IList<GameObject> input)
        {
            if (outputPath == "")
            {
                string name = "";
                name = input[input.Count - 1].name;
                outputPath = Application.dataPath.Replace("Assets", name);
            }
            return base.preProcess(input);
        }

        protected override void process()
        {
            try
            {
                ///TO DO : convert to pixyz units
                //foreach(Matrix4 matrix in Context.pixyzMatrices.list)
                //{
                //    matrix[0][0] *= 1000.0f;
                //    //matrix[0][3] *= 1000.0f;
                //    matrix[1][1] *= 1000.0f;
                //    //matrix[1][3] *= 1000.0f;
                //    matrix[2][2] *= -1000.0f;
                //    //matrix[2][3] *= 1000.0f;
                //}
                NativeInterface.ExportMeshes(Context.pixyzMeshes, Context.pixyzMatrices, outputPath.ToString());
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[Error] {e.Message} /n {e.StackTrace}");
            }
        }

        protected override void postProcess()
        {
            _output = (IList<GameObject>)Input;
        }        
    }
}