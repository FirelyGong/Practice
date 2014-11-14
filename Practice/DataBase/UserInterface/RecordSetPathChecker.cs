using System;

namespace Practice.DataBase.UserInterface
{
    public class RecordSetPathChecker
    {
        public const int PathMaxDeepth = 5;
        public const int SingleLevelPathMaxlength = 50;

        public static void CheckPath(string[] path)
        {
            if (path.Length > PathMaxDeepth)
            {
                throw new Exception("The path exceeds max path deepth!");
            }

            for (int i = 0; i < path.Length; i++)
            {
                if (path[i].Length > SingleLevelPathMaxlength)
                {
                    throw new Exception("The single level path exceeds max length!");
                }
            }
        }
    }
}
