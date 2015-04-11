#r "Tools/FAKE/tools/FakeLib.dll"

open Fake
open Fake.AssemblyInfoFile

let version = getBuildParamOrDefault "version" "3.0.2.0"

Target "BuildApp" (fun _ ->
    CreateCSharpAssemblyInfo "./src/GlobalAssemblyInfo.cs"
            [Attribute.Version version
             Attribute.FileVersion version]
    
    MSBuildRelease "./build" "build" ["./SQLite.Net.sln"]
        |> Log "AppBuild-Output: "
)

Target "Package" (fun _ ->
    let path = sprintf "./Release/SQLite.Net-%s.zip" version
    CreateDir "Release"
    ZipHelper.CreateZip "build" path "" ZipHelper.DefaultZipLevel false !!("./build/**")
)

Target "Clean" (fun _ -> 
    CleanDirs ["./Release/"; "./build/"]
)

Target "Help" <| fun () ->
    printfn ""
    printfn "  Please specify the target by calling 'build <Target>'"
    printfn ""
    printfn "  * BuildApp   - Build everything"
    printfn "  * Package    - Build and package a new release"

Target "Root" DoNothing

"Clean"
    ==> "BuildApp"
    ==> "Package"

RunTargetOrDefault "Help"
