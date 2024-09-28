namespace Tracker
{
    internal class SonarLintSample
    {
        // TODO: Say hello to SonarLint!

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Info Code Smell", "S1135:Track uses of \"TODO\" tags", Justification = "<Pending>")]
        protected SonarLintSample() { }

        public static bool SomeStupidMethod(bool useOptionb)
        {
            bool rtnVal = false;

            if (useOptionb)
            {
                rtnVal = true;
            }
            else
            {
                rtnVal = false;
            }

            return rtnVal;
        }

        // this works in vscode
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite", Justification = "<Pending>")]

        // the pragma has to be on the method, not the bad block of code (interesting)
#pragma warning disable S2190 // Loops and recursions should not be infinite
        public static void AnotherMethod()
        {
            var i = 0;
            var result = 0;

            while (true) // Noncompliant: the program will never stop
            {
                result += i;
                i++;
            }
        }
#pragma warning restore S2190 // Loops and recursions should not be infinite
    }
}
