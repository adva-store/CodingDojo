<?php
/**
 * Es lädt das HTML Template
 */
function init()
{
    require config('template_path') . '/template.php';
}

/**
 * Es lädt CSS Dateien
 */
function load_css($file = '')
{
    $url = config('site_url') . '/' . config('css_path') . '/' . $file . '.css';
    $path = getcwd() . '/' . config('css_path') . '/' . $file . '.css';
    if (file_exists($path)) {
        echo '<link href="'.$url.'" rel="stylesheet" type="text/css" />';
    }
}
/**
 * Es lädt JS Dateien
 */
function load_js($file = '')
{
    $url = config('site_url') . '/' . config('js_path') . '/' . $file . '.js';
    $path = getcwd() . '/' . config('js_path') . '/' . $file . '.js';
    if (file_exists($path)) {
        echo '<script src="'.$url.'"></script>';
    }
}
?>