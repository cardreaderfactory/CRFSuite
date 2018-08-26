<?php

$debug = false;     /* if true, allows usage from browsers with get and post */

/*
   Use cases:
    1. recovery code
    2. specific firmware (nbuild must be present) -> fallthrough to generic firmware if not found
    3. generic firmware (name and cpu must be present)

   When writing a new firmware, ensure that the following fields are present:

    1. for recovery firmwares, 'recoveryCode' must be present. everything else is ingored
    2. for build specific firmware, 'build' must be present. 'device' and 'cpu' are also considered
    3. for generic firmwares, 'device' and 'cpu' must be present and 'build' must be set to '*'

   Testing scenarios:

   http://www.cardreaderfactory.com/crfsuite/getfirmware.php?command=check&recoveryCode=__v007_168_show
   http://www.cardreaderfactory.com/crfsuite/getfirmware.php?command=check&build=000069

*/

function printd($severity, $message)
{
    global $debug;
    if (!$debug)
        return;

    if ($severity == "error")
        $color = "#ff0000";
    else
        $color = "#999999";
    printf("<p style=\"color: " . $color . "\">" . $message . "</p>");
}

function sql_query($query)
{
    global $sql_debug;
    $result = mysql_query($query);

    if ($sql_debug)
    {
        if (!$result)
            printf("<p style=\"color: #ff0000;\">" . $query . "<br> query failed.</p>");
        else
            printf("<p style=\"color: #999999;\">" . $query . "</p>");
    }

    return $result;
}

/**
 * Strip html tags from the data
 *
 * @param mixed $var variable to strip tags from
 * @return mixed filtered variable
 */
function fn_strip_tags(&$var)
{

    if (!is_array($var)) {
        return (strip_tags($var));
    } else {
        $stripped = array();
        foreach ($var as $k => $v) {
            $sk = strip_tags($k);
            if (!is_array($v)) {
                $sv = strip_tags($v);
            } else {
                $sv = fn_strip_tags($v);
            }
            $stripped[$sk] = $sv;
        }
        return ($stripped);
    }
}

/**
 * Sanitize input data
 *
 * @param mixed $data data to filter
 * @return mixed filtered data
 */
function fn_safe_input($data)
{
    if (defined('QUOTES_ENABLED')) {
        $data = fn_strip_slashes($data);
    }

    return fn_strip_tags($data);
}

/**
 * Strip slashes
 *
 * @param mixed $var variable to strip slashes from
 * @return mixed filtered variable
 */
function fn_strip_slashes($var)
{
    if (is_array($var)) {
        $var = array_map('fn_strip_slashes', $var);
        return $var;
    }

    return (strpos($var, '\\\'') !== false || strpos($var, '\\\\') !== false || strpos($var, '\\"') !== false) ? stripslashes($var) : $var;
}

function getRealIpAddr()
{
    if (!empty($_SERVER['HTTP_CLIENT_IP']))   //check ip from share internet
    {
      $ip=$_SERVER['HTTP_CLIENT_IP'];
    }
    elseif (!empty($_SERVER['HTTP_X_FORWARDED_FOR']))   //to check ip is pass from proxy
    {
      $ip=$_SERVER['HTTP_X_FORWARDED_FOR'];
    }
    else
    {
      $ip=$_SERVER['REMOTE_ADDR'];
    }
    return $ip;
}

function lookup_cpu($device)
{
    $row = false;

    if ($device['build'] == "")
    {
        //printf("lookupcpu aborted");
        return $device;
    }

    $query = sprintf("SELECT `cpu`, `device` FROM devices WHERE `build`='%s'", mysql_real_escape_string($device['build']));
    $result = sql_query($query);
    if ($result)
        $row = mysql_fetch_assoc($result);
    if (!$row)
        return $device;

    if (!$device['cpu'] && $row['cpu'])
        $device['cpu'] = $row['cpu'];

    if (!$device['device'] && $row['device'])
        $device['device'] = $row['device'];

    return $device;
}

/* updates the device params in the device list */
function log_device($device, $confirmed)
{
    printd(1, "log device() build = " . $device['build'] . " cpu = " . $device['cpu']);
    $row = false;
    if ($device['build'] == "" || $device['cpu'] == "")
        return;

    $query = sprintf("SELECT * FROM devices WHERE `build`='%s'",
                    mysql_real_escape_string($device['build']));

    $result = sql_query($query);
    if ($result)
        $row = mysql_fetch_assoc($result);
    if (!$row)
    {
        $query = sprintf("INSERT INTO `devices` (`device`, `cpu`, `fuses`, `functionality`, `build`, `fw`, `added by`, `added on`) VALUES ('%s','%s','%s','%s','%s','%s','%s','%s');",
                mysql_real_escape_string($device['device']),
                mysql_real_escape_string($device['cpu']),
                mysql_real_escape_string($device['fuses']),
                mysql_real_escape_string($device['functionality']),
                mysql_real_escape_string($device['build']),
                mysql_real_escape_string($device['firmware']),
                mysql_real_escape_string(getRealIpAddr()),
                date('Y-m-d H-i-s')
                );

        sql_query($query);
        return;
    }

    $comma = "";
    $set = "";

    if ($row['device'] == "")
    {
        $set .= " `device`='" . mysql_real_escape_string($device['device']) . "' ";
        $comma = ",";
    }

    if ($row['cpu'] == "")
    {
        $set .= $comma . " `cpu`='" . mysql_real_escape_string($device['cpu']) . "' ";
        $comma = ",";
    }

    if ($row['fuses'] == "" || ($row['fuses'] != $device['fuses'] && $device['fuses'] != ""))
    {
        $set .= $comma . " `fuses`='" . mysql_real_escape_string($device['fuses']) . "' ";
        $comma = ",";
    }

    if ($row['functionality'] == "" || ($row['functionality'] != $device['functionality'] && $device['functionality'] != ""))
    {
        $set .= " `functionality`='" . mysql_real_escape_string($device['functionality']) . "' ";
        $comma = ",";
    }

    if ($confirmed)
    {
        $set .= $comma . " `fw updates`='" . mysql_real_escape_string($row['fw updates']+1) . "' ";
        $comma = ",";
    }

    if ($confirmed && $device['firmware'] != "")
    {
        $set .=  $comma . " `fw`='" . mysql_real_escape_string($device['firmware']) . "' ";
        $comma = ",";
    }

    if ($set != "")
    {
        $query = "UPDATE `devices` SET " . $set . " WHERE `index`='" . mysql_real_escape_string($row['index']) . "' LIMIT 1 ;";
        $result = sql_query($query);
    }

//    printf("<br>" . $query);
//    if (!$result)
//        printf("<br> query failed");


}

function build_query($query, $column_name, $v)
{
    if (!$v)
        return $query;

    if ($query != "")
        $query .= "AND ";

    $query .= " `" . $column_name . "`='" . mysql_real_escape_string($v) . "' ";
    return $query;
}

function sql_fetch_firmware($query, $device)
{
    if (!$query)
        return false;

    $row = false;
    $result = sql_query("SELECT * FROM firmware WHERE " . $query);
    if (!$result)
        return false;

    while ($row = mysql_fetch_assoc($result))
    {

        //printd(1, "download count: " . $row['dl']);
        //printd(1, "max download count: " . $row['dlMax']);

        /* download check */
        if ($row['dl'] >= $row['dlMax'] || $row['dlOk'] >= $row['dlOkMax'])
        {
            printd("error" , "max download count exceeded; data dropped");
            continue;
        }

        /* firmware version check */
        if ($_REQUEST['command'] != "complete"  /* as CRFSuite reports the new version on "complete". Without this condition it will never mark it as complete */
            && $device['firmware'] && $device['firmware'] != "unknown")
        {
            if ($device['firmware'] >= $row['fw'])
            {
                printd("error" , "firmware " . $row['fw'] . " found but is the same or too old");
                continue;
            }

            if (($row['fwMin'] && $device['firmware'] < $row['fwMin']) ||
                ($row['fwMax'] && $device['firmware'] > $row['fwMax']))
            {
                printd("error" , "firmware " . $row['fw'] . " found but it is accepted for [" . $row['fwMin'] . ".." . $row['fwMax'] . "]  and currentFw = " . $device['firmware']);
                continue;
            }
        }

        return $row;
    }

    return $row;
}


function get_firmware($device)
{
    $row = false;

    /* check if using recovery code */
    $query = build_query("", 'recoveryCode', $device['recoveryCode']);
    if ($query != "")
        return sql_fetch_firmware($query, $device);

    if ($device['build'])
    {
        /* looking for device specific firmware. at least the build must match */
        $query = build_query("", 'device', $device['device']);
        $query = build_query($query, 'cpu', $device['cpu']);
        $query = build_query($query, 'build', $device['build']);
        $row = sql_fetch_firmware($query, $device);
        if ($row)
            return $row;
    }

    if ($device['device'] && $device['cpu'])
    {
        /* looking for generic builds. name and cpu must match */
        $query = build_query("", 'device', $device['device']);
        $query = build_query($query, 'cpu', $device['cpu']);
        $query = build_query($query, 'build', "*");
        $row = sql_fetch_firmware($query, $device);
    }

    return $row;
}


function show_result($table, $result)
{
    if (!$result)
        return;
    
    $num=mysql_num_rows($result);
    if ($num == 0)
    {
        printd("error", "nothing in table " . $table);
        return;
    }

    printf("<h1> " . $table . "</h1>");
    // printing header
    printf("<table border=\"0\" cellspacing=\"2\" cellpadding=\"2\">"); 
    
    $fields = mysql_num_fields($result);
       
    printf("<tr class=\"header\">");        
    for ($i = 0; $i < $fields; $i++)
    {
        $name = mysql_field_name($result,$i);
        $req = $_REQUEST;
        if ($req['sort'] == $name && $req['desc'] != "DESC")
            $req['desc'] = "DESC";
        else
            $req['desc'] = "";
        
        $req['sort'] = $name;
        $req['table'] = $table;
        printf("<th nowrap=\"nowrap\"><a href=\"" . $_SERVER['PHP_SELF'] . "?" . array2url($req) ."\">" .$name . "</a></th>");
    }    
    printf("</tr>");        
    
    $i = 0;
    $row = false;
    while ($row = mysql_fetch_assoc($result))
    {
        $i++;
        if ($i % 2)
            printf("<tr class=\"odd-row\">");        
        else
            printf("<tr class=\"even-row\">");        
        
        foreach ($row as $value)
        {
            printf("<td nowrap=\"nowrap\">" . $value . "</td>");
        }
        printf("</tr>");        
    }
    printf("</table><br>");        


}

function show_table($table, $query)
{
    
    $sql = "SELECT * FROM " . $table;
    if ($query != "")
        $sql .= " WHERE " . $query;
    if ($_GET['table'] == $table && $_GET['sort'] != "")
    {        
        $sql .= " ORDER BY " . $_GET['sort'];
        if ($_GET['desc'] == "DESC")
            $sql .= " DESC";
    }        
        
    $result = sql_query($sql);
    show_result($table, $result);       
}

function search_device($device)
{
?>

<style type="text/css">
.header {
	color: #FFF;
	background-color: #000;
}
.odd-row {
	background-color: #CCC;
}
.even-row {
	background-color: #999;
}
</style>
<body>
    
<?php

    /* looking for device specific firmware. at least the build must match */
    if ($device['build'] != "")
        $query = build_query("", 'build', $device['build']);
    
    if ($_GET['table'] != "")
    {
        show_table($_GET['table'], $query);
        return;
    }
        
    $tables = array ( "devices", "log", "firmware" );
    foreach ($tables as $t)
    {
        show_table($t, $query);        
    }    
?>
</body>
<?php

}




function check_firmware($device)
{
    $row = get_firmware($device);
    log_request($device, "");

    if (!$row)
        printf("fw=n/a^message=No firmware available for this device.");
    else
        printf("fw=%s^message=%s",$row['fw'], $row['release notes']);
}

function send_firmware($device)
{
    $row = get_firmware($device);

    if (!$row)
        return;

    log_request($device, $row['fw']);

    if (!array_key_exists( "fw", $row))
        return;

    printf(strtoupper(sha1($row['data'])) . $row['data']);

    $query = "UPDATE `firmware` SET `dl`='" .
                  mysql_real_escape_string($row['dl']+1) .
                  "' WHERE `index`='" .
                  mysql_real_escape_string($row['index']) .
                  "' LIMIT 1 ;";

    $result = sql_query($query);

//    if (!$result)
//        printf("<br>update failed");

//    printf("functionality = " . $device['functionality'] . "<br>");
}

function init_database()
{
    $config['db_host'] = 'localhost';
    $config['db_name'] = 'msrvcom1_crf';
    $config['db_user'] = 'msrvcom1_crf';
    $config['db_password'] = 'Ek%AQ1]*&UiS';
    $config['db_type'] = 'mysql';

    mysql_connect($config['db_host'],$config['db_user'],$config['db_password']);
    @mysql_select_db($config['db_name']) or die( "Unable to select database");

    $query="CREATE TABLE IF NOT EXISTS `log` (
              `index` int(11) NOT NULL AUTO_INCREMENT,
              `date` datetime NOT NULL,
              `ip` char(16) NOT NULL,
              `command` char(10) NOT NULL,
              `recoveryCode` char(20) NOT NULL,
              `device` char(10) NOT NULL,
              `functionality` char(8) NOT NULL,
              `build` char(8) NOT NULL,
              `cpu` char(4) NOT NULL,
              `fuses` char(10) NOT NULL,
              `old fw` char(8) NOT NULL,
              `new fw` char(8) NOT NULL,
              `memory` int(11) NOT NULL,
              `hw` tinyint(4) NOT NULL,
              `readMode` char(8) NOT NULL,
              `cmd` mediumtext NOT NULL,
              PRIMARY KEY (`index`),
              KEY `build` (`build`)
            ) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;";

    if (sql_query($query) === false)
        die( "Unable to initialise table 'log'");

    $query = "CREATE TABLE IF NOT EXISTS `firmware` (
              `index` int(11) NOT NULL auto_increment,
              `device` char(10) NOT NULL,
              `cpu` char(4) NOT NULL,
              `build` char(8) NOT NULL,
              `fw` char(8) NOT NULL,
              `fwMin` char(8) NOT NULL,
              `fwMax` char(8) NOT NULL,
              `recoveryCode` char(20) NOT NULL,
              `dl` int(11) NOT NULL,
              `dlMax` int(11) NOT NULL,
              `dlOk` int(11) NOT NULL,
              `dlOkMax` int(11) NOT NULL,
              `release notes` text NOT NULL,
              `data` mediumtext NOT NULL,
              UNIQUE KEY `index` (`index`),
              KEY `device` (`device`)
            ) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;";

    if (sql_query($query) === false)
        die( "Unable to initialise table 'firmware'");

    $query="CREATE TABLE IF NOT EXISTS `devices` (
              `index` int(11) NOT NULL AUTO_INCREMENT,
              `added by` text NOT NULL,
              `added on` datetime NOT NULL,
              `purchased` date NOT NULL,
              `warranty` date NOT NULL,
              `device` char(10) NOT NULL,
              `functionality` char(8) NOT NULL,
              `cpu` char(4) NOT NULL,
              `fuses` char(10) NOT NULL,
              `build` char(8) NOT NULL,
              `fw` char(8) NOT NULL,
              `fw updates` int(11) NOT NULL,
              `email` varchar(255) NOT NULL,
              `key` char(64) NOT NULL,
              `upgrade` char(20) NOT NULL,
              `pass2` char(6) NOT NULL,
              `notes` text NOT NULL,
              UNIQUE KEY `index` (`index`),
              UNIQUE KEY `build` (`build`),
              KEY `email` (`email`)
            ) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1;";

    if (sql_query($query) === false)
        die( "Unable to initialise table 'devices'");

}


function array2url($array_name)
{
    if(!is_array($array_name))
        return "";

    $st = "";

    foreach($array_name as $k=>$v)
    {
       if ($st != "")
           $st .= "&";

       $st .= $k . "=" . $v;
    }

    return $st;
}

function log_request($device, $newFirmware)
{
    $query="INSERT INTO `log` (`command`, `device`, `functionality`, `build`, `cpu`, `fuses`, `old fw`, `new fw`, `memory`, `hw`, `readMode`, `recoveryCode` , `ip`, `date`, `cmd`)
            VALUES ('". $_REQUEST['command'] . "', '" .
                    $device['device'] . "', '" .
                    $device['functionality'] . "', '" .
                    $device['build'] . "', '" .
                    $device['cpu'] . "', '" .
                    $device['fuses'] . "', '" .
                    $device['firmware'] . "', '" .
                    $newFirmware . "', '" .
                    $device['memory'] . "', '" .
                    $device['hw'] . "', '" .
                    $device['readMode'] . "', '" .
                    $device['recoveryCode'] . "', '" .
                    getRealIpAddr() . "', '" .
                    date('Y-m-d H-i-s') . "', '" .
                    array2url($_REQUEST) . "');";

    if (sql_query($query) === false)
        die( "Unable to write in database");

}


function confirm_update($device)
{
    log_request($device, "");

    $row = get_firmware($device);

    if (!$row)
        return false;

    $query = "UPDATE `firmware` SET `dlOk`='" .
                  mysql_real_escape_string($row['dlOk']+1) .
                  "' WHERE `index`='" .
                  mysql_real_escape_string($row['index']) .
                  "' LIMIT 1 ;";

    $result = sql_query($query);
    return true;
}


function process_params()
{
    $device = array();

    $device['device']           = array_key_exists( "device",           $_REQUEST) ? $_REQUEST['device']           : "";
    $device['recoveryCode']     = array_key_exists( "recoveryCode",     $_REQUEST) ? $_REQUEST['recoveryCode']     : "";
    $device['functionality']    = array_key_exists( "functionality",    $_REQUEST) ? $_REQUEST['functionality']    : "";
    $device['build']            = array_key_exists( "build",            $_REQUEST) ? $_REQUEST['build']            : "";
    $device['cpu']              = array_key_exists( "cpu",              $_REQUEST) ? $_REQUEST['cpu']              : "";
    $device['fuses']            = array_key_exists( "fuses",            $_REQUEST) ? $_REQUEST['fuses']            : "";
    $device['firmware']         = array_key_exists( "firmware",         $_REQUEST) ? $_REQUEST['firmware']         : "";
    $device['memory']           = array_key_exists( "memory",           $_REQUEST) ? $_REQUEST['memory']           : 0;
    $device['hw']               = array_key_exists( "hw",               $_REQUEST) ? $_REQUEST['hw']               : 0;
    $device['readMode']         = array_key_exists( "readMode",         $_REQUEST) ? $_REQUEST['readMode']         : "";

    return $device;
}

function main()
{
    global $debug, $sql_debug;

    if ($debug)
    {
        $_REQUEST = fn_safe_input(array_merge($_POST, $_GET));
        /* show sql statements for other browsers than CRFSuite */
        if (strstr($_SERVER['HTTP_USER_AGENT'], "CRFSuite") == false)
            $sql_debug = true;
        else
            $sql_debug = false;
    }
    else
    {
         $_REQUEST = fn_safe_input($_POST);
         if (strstr($_SERVER['HTTP_USER_AGENT'], "CRFSuite") == false)
         {
            printf("access denied<br>");
            return;
         }
         $sql_debug = false;
    }

    if (!array_key_exists( "command", $_REQUEST))
    {
        print("incorrect usage");
        return;
    }

    $device = process_params();
    $command = $_REQUEST["command"];
    date_default_timezone_set('UTC');
    init_database();
    $device = lookup_cpu($device);

    switch ($_REQUEST['command'])
    {
        case 'check':
            check_firmware($device);
            log_device($device, false);
            break;
        case 'download':
            send_firmware($device);
            log_device($device, false);
            break;
        case 'complete':
            $ok = confirm_update($device);
            log_device($device, $ok);
            break;
        case 'search':
            search_device($device);
            break;
        default:
            printf("invalid command");
            break;
    }

}

main();

?>
