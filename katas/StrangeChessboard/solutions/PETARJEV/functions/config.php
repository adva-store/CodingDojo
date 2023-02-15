<?php

function config($key = '')
{
    $config = [
        'name' => '',
        'site_url' => '',
        'template_path' => 'template',
        'version' => 'v1.0',
    ];

    return isset($config[$key]) ? $config[$key] : null;
}
?>