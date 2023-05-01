<?php

use PhpCsFixer\Config;
use PhpCsFixer\Finder;

return (new Config())->setFinder(
        Finder::create()
            ->in([
                __DIR__.'/app',
                __DIR__.'/config',
                __DIR__.'/database',
                __DIR__.'/resources',
                __DIR__.'/routes',
                __DIR__.'/tests',
            ])
            ->name('*.php')
            ->notName('*.blade.php')
            ->ignoreDotFiles(true)
            ->ignoreVCS(true)
    )
    ->setRules([
        '@PSR1' => true,
        '@PSR2' => true,
        '@PSR12' => true,
        'array_syntax' => ['syntax' => 'short'],
        'trailing_comma_in_multiline' => true,
        'general_phpdoc_annotation_remove' => true,
        'no_empty_phpdoc' => true,
        'no_superfluous_phpdoc_tags' => true,
        'ordered_class_elements' => true,
        'phpdoc_trim_consecutive_blank_line_separation' => true,
        'phpdoc_trim' => true,
        'no_unused_imports' => true,
    ])
    ->setRiskyAllowed(true)
    ->setUsingCache(true);
