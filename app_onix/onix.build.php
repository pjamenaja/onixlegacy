<?php

declare(strict_types=1);

$product = $argv[1];

$_ENV['BIN_DIR'] = dirname(__FILE__);
chdir($_ENV['BIN_DIR']);

print("Creating build.php ...\n");

$srcRoot = $_ENV['BIN_DIR'] . '/onix_server/scripts';

$fname = "$srcRoot/build.php";
$oh = fopen($fname, "w") or die("Unable to open file [$fname]!");
date_default_timezone_set('Asia/Bangkok');
$dt = '1.0.3_' . date("mdYHis");
$tmp = '$APP_VERSION_LABEL = ' . "'$dt'";
$app_product = '$APP_BUILT_PRODUCT = ' . "'$product'";

$stmt = <<<EOD
<?php
/* 
Purpose : Auto generated built time file (DO NOT MODIFY)
Created By : Seubpong Monsar
*/

$tmp;
$app_product;

?>
EOD;

fwrite($oh, $stmt);
fclose($oh);

#===
print("Creating CBuild.cs ...\n");
$srcRoot = $_ENV['BIN_DIR'] . '/onix_client/OnixClientDll/Helper';
$fname = "$srcRoot/CBuild.cs";
$oh = fopen($fname, "w") or die("Unable to open file [$fname]!");

$tmp = '$APP_VERSION_LABEL = ' . "'$dt'";

$stmt = <<<EOD
/*
* This is auto generated class, DO NOT modify
*/
using System;

namespace Onix.Client.Helper
{
   public static class CBuild
   {
       public static String BuildVersion = "$dt";
       public static String Product = "$product";
   }
}
EOD;

fwrite($oh, $stmt);
fclose($oh);
#===
#exit(0);

$commands = [
    'MSBuild.exe onix_client/OnixClientCenter/OnixClientCenter.sln',
]; 

ExecuteFunctions($commands);
CopyFiles();

exit(0);

function ExecuteFunctions($commands)
{
    foreach ($commands as $cmd)
    {
        printf("Executing command [$cmd]\n");   

        exec($cmd, $outputs, $retCode);     
        if ($retCode)
        {
            throw new Exception('Error : ' . $retCode);
        }
    }
}

function CopyFiles()
{    
    $zips1 = [
        ['/.+\.exe$/', 'onix_client/OnixClientCenter', 'OnixCenter'],
        ['/.+\.dll$/', 'onix_client/OnixClientCenter', 'OnixCenter'],
        ['/.+\.exe/', 'dll_from_lib', 'OnixCenter'] 
    ];

    $zips = [ ['OnixCenter.zip', $zips1] ];

    foreach ($zips as $zip)
    {
        list($zipName, $specs) = $zip;
        $zip_name = $zipName;

        $zip = new ZipArchive();
        if ($zip->open($zip_name, ZipArchive::CREATE) !== TRUE) 
        {
            throw new Exception("Create zip [$zip_name] file error!!!");
        }
        
        foreach ($specs as $cfg)
        {
            list($pattern, $srcDir, $destDir) = $cfg;

            $iterator = new RecursiveIteratorIterator(new RecursiveDirectoryIterator($srcDir));

            foreach ($iterator as $key => $value) 
            {        
                if (!is_file($key))
                {
                    continue;
                }

                $name = basename($key);
                if (preg_match($pattern, $name, $matches)) 
                {
                    if (!preg_match('/\.vshost\./', $name, $matches)) 
                    {
                        $zip->addFile($key, "$destDir/$name");
                    }
                }                  
            }                       
        }

        $zip->close();
        printf("Created zip file [%s] done\n", $zip_name);
    }
}

?>
