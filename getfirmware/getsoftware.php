<?php

$debug = true;

if (strstr($_SERVER['HTTP_USER_AGENT'], "CRFSuite") == false && $debug == false)
    printf("invalid command");
else
//    printf("version=2.4.9.5^link=http://localhost/CRFSuite_2.4.9.5.exe");
    printf("version=3.1.0^link=http://www.cardreaderfactory.com/download/crfsuite/CRFSuite_3.1.0.exe");

?>
