<?php

function config($key = '')
{
    $config = [
        'name' => 'Berlin  Clock',
        'site_url' => '',
        'template_path' => 'template',
        'css_path' => 'assets/css',
        'js_path' => 'assets/js',
        'version' => 'v1.0',
    ];

    return isset($config[$key]) ? $config[$key] : null;
}
?>