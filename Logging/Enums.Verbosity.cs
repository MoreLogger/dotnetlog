
using System;

namespace Logging.Enums
{
    public enum Verbosity
    {
        eTrace,
        eNone = eTrace,
        eFatals,
        eErrors,
        eWarnings,
        eInfos,
        eDebugs
    }
}
