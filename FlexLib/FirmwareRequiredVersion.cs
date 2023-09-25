﻿// This file has been automatically generated by Jenkins
// The required_version has been populated based on the most
// recent version of Firmware that was built by Jenkins

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;


namespace Flex.Smoothlake.FlexLib
{
    public static class FirmwareRequiredVersion
    {
        // See FlexVersion.cs
        private static ulong required_version = FlexVersion.Parse("3.1.8.145");
        public static ulong RequiredVersion
        {
            get { return required_version; }
        }

        // For automatic builds, Jenkins will set branch_name based on current branch
        private static String branch_name = "dev";   //"Beta", "Alpha", blank for master.
        public static String BranchName
        {
            get { return branch_name; }
        }

    }
}