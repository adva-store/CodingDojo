<?php
/**
 * SUPER DIRTY SOLUTION
 * EXTREMLY UGLY 
 **/

// DEFINITIONS
define('title', 'Langtons Ant');
define('width', 100);
define('height', 100);
define('pixelsize', 6);
define('langtonsant_image', 'langtonsant.png');
// DIRECTION
// 0 = up, 1 = left, 2 = down, 3 = right
define('dir', 0);


// VARIABLES
// STARTING POINT CENTERED
$x = width/2;
$y = height/2;

// DIRECTION
$dir = dir;

// GRID
$antgrid = array();

/**
 * MAIN FUNCTION TO DRAW THE GRID
 **/
function drawCanvas($x,$y,$dir) {
    
    // STEP COUNT
    $step_count = 0; 

    // LANGTONS ANT ALGORITHM
    while(0 <= $x && $x <= width && 0 <= $y && $y <= height){
        if(isset($antgrid[$x][$y])){
            unset($antgrid[$x][$y]);
            $dir = ($dir + 3) % 4;
        } else {
            $antgrid[$x][$y] = true;
            $dir = ($dir + 1) % 4;
        }
        switch($dir){
            case 0: $y++; break;
            case 1: $x--; break;
            case 2: $y--; break;
            case 3: $x++; break;
        }
        $step_count++;
    }

    $pagecontent = "<div class='antCanvas' style='width:".(width*pixelsize)."; height:".(height*pixelsize).";'>";
    for($x = 0; $x < width; $x++){  
        for($y = 0; $y < height; $y++){
            if(isset($antgrid[$x][$y])){
                $pagecontent .= "<span class='pixel' style='left:".($x*pixelsize)."; top:".($y*pixelsize)."; width:".pixelsize."px; height:".pixelsize."px; background-color:#404040;'></span>";
                //$step_count++;
            }
        }
    }
    $pagecontent .= "</div>";
    $pagecontent .= "<div class='infoblock'>";
    $pagecontent .= "<p>Ant Grid: w=".width." h=".height."</p>";
    $pagecontent .= "<p>Pixel Size: ".pixelsize."</p>";
    $pagecontent .= "<p>Steps: ".$step_count."</p>";
    $pagecontent .= "</div>";

    echo $pagecontent;

}

/**
 * ADDITIONAL CREATE IMG FUNCTION TO PROOF THE OUTPUT
 **/
function createImage($x,$y,$dir) {
    while(0 <= $x && $x <= width && 0 <= $y && $y <= height){
        if(isset($antgrid[$x][$y])){
            unset($antgrid[$x][$y]);
            $dir = ($dir +3) % 4;
        } else {
            $antgrid[$x][$y] = true;
            $dir = ($dir + 1) % 4;
        }
        switch($dir){
            case 0: $y++; break;
            case 1: $x--; break;
            case 2: $y--; break;
            case 3: $x++; break;
        }
    }
    $image = imagecreatetruecolor(width, height);
    $white = imagecolorallocate($image, 255, 255, 255);
    $black = imagecolorallocate($image, 40, 40, 40);
    imagefilledrectangle($image, 0, 0, width, height, $white);
    for($x = 0; $x < width; $x++){  
        for($y = 0; $y < height; $y++){
            if(isset($antgrid[$x][$y])){
                imagesetpixel($image, $x, $y, $black);
            }
        }
    }

    // SAVE IMAGE
    imagepng($image, langtonsant_image);
    imagedestroy($image);
}


/**
 * Would have been the preferred solution to come up with an object oriented solution
 * Just a rough sketch did not found the time to create it properly - please apologize
 */

/*
require_once(__DIR__."/src/Ant.php");
require_once(__DIR__."/src/Antgrid.php");

$grid = new Antgrid(200,200);
$ant = new Ant($grid);

$i=0;
while(true) {
    $ant->step();
    $i++;
}
*/

//




?>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head>
    <title><?=title?></title>
    <link rel="stylesheet" href="css/style.css" />
</head>
<body>
    <div class="content">
        <?php
            drawCanvas($x,$y,$dir);
            createImage($x,$y,$dir);
        ?>
        <div id="antCanvas"></div>
    </div>
</body>
</html>