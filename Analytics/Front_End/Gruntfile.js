'use strict';
module.exports = function (grunt) {

    // Load grunt tasks automatically
    require('load-grunt-tasks')(grunt);

    // Show grunt task time
    require('time-grunt')(grunt);

    require('grunt-html2js')(grunt);

    // Configurable paths for the app
    var appConfig = {
        appName: 'app',
        dist: {'admin':'../dist/admin', 'analytics':'../dist/analytics', 'index':'../dist/index'}
    };

    // Grunt configuration
    grunt.initConfig({

        // Project settings
        app: appConfig,

        // The grunt server settings
        connect: {
            options: {
                port: 3300,
                hostname: '192.168.1.66',
                livereload: 35730
            },
            livereload: {
                options: {
                    open: true,
                    middleware: function (connect) {
                        return [
                            connect.static('.tmp'),
                            connect().use(
                                '/bower_components',
                                connect.static('./bower_components')
                            ),
                            connect.static(appConfig.app)
                        ];
                    }
                }
            },
            dist: {
                options: {
                    open: true,
                    base: '../dist/'
                }
            }
        },
        // Compile less to css
        less: {
            development: {
                src: ['app/less/style.less'],
                dest: 'app/styles/style.css',
                options: {
                    compress: true,
                    optimization: 2
                }
            }
        },
        // Watch for changes in live edit
        watch: {
            styles: {
                files: ['app/less/**/*.less'],
                tasks: ['less', 'copy:styles'],
                options: {
                    nospawn: true,
                    livereload: '<%= connect.options.livereload %>'
                },
            },
            js: {
                files: ['<%= app.appName %>/scripts/{,*/}*.js'],
                options: {
                    livereload: '<%= connect.options.livereload %>'
                }
            },
            livereload: {
                options: {
                    livereload: '<%= connect.options.livereload %>'
                },
                files: [
                    '<%= app.appName %>/**/*.html',
                    '.tmp/styles/{,*/}*.css',
                    '<%= app.appName %>/images/{,*/}*.{png,jpg,jpeg,gif,webp,svg}'
                ]
            }
        },
        babel: {
                options: {
                    sourceMap: true,
                    presets: ['es2015']
                },
                dist: {
                    files: {
                        '.tmp/concat/scripts/scripts.js': '.tmp/concat/scripts/scripts.js'
                    }
                }
        },
        uglify: {
            options: {
                mangle: false
            }
        },
        // Clean dist folder
        clean: {
            dist: {
                files: [{
                    dot: true,
                    src: [
                        '.tmp',
                        '<%= app.dist %>/**/{,*/}*',
                        '!<%= app.dist %>/.git*'
                    ]
                }]
            },
            server: '.tmp'
        },
        // Copies remaining files to places other tasks can use
        copy: {
            index: {
                files: [
                    {
                        expand: true,
                        dot: true,
                        cwd: '<%= app.appName %>',
                        dest: '<%= app.dist.index %>',
                        src: [
                            '*.{ico,png,txt}',
                            '.htaccess',
                            'index.html',
                            'views/common/{,*/}*.html',
                            'views/index/{,*/}*.html',
                            'styles/img/*.*',
                            'images/{,*/}*.*'
                        ]
                    },
                    {
                        expand: true,
                        dot: true,
                        cwd: 'bower_components/fontawesome',
                        src: ['fonts/*.*'],
                        dest: '<%= app.dist.index %>'
                    },
                    {
                        expand: true,
                        dot: true,
                        cwd: 'bower_components/bootstrap',
                        src: ['fonts/*.*'],
                        dest: '<%= app.dist.index %>'
                    },
                    {
                        expand: true,
                        dot: true,
                        cwd: 'bower_components/angular-ui-grid',
                        src: ['*.eot', '*.svg', '*.ttf', '*.woff'],
                        dest: '<%= app.dist.index %>/styles'
                    },
                    {
                        expand: true,
                        dot: true,
                        cwd: 'app/fonts/pe-icon-7-stroke/',
                        src: ['fonts/*.*'],
                        dest: '<%= app.dist.index %>'
                    },
                ]
            },
            admin: {
                files: [
                    {
                        expand: true,
                        dot: true,
                        cwd: '<%= app.appName %>',
                        dest: '<%= app.dist.admin %>',
                        src: [
                            '*.{ico,png,txt}',
                            '.htaccess',
                            'admin.html',
                            'views/common/{,*/}*.html',
                            'views/admin/{,*/}*.html',
                            'styles/img/*.*',
                            'images/{,*/}*.*'
                        ]
                    },
                    {
                        expand: true,
                        dot: true,
                        cwd: 'bower_components/fontawesome',
                        src: ['fonts/*.*'],
                        dest: '<%= app.dist.admin %>'
                    },
                    {
                        expand: true,
                        dot: true,
                        cwd: 'bower_components/bootstrap',
                        src: ['fonts/*.*'],
                        dest: '<%= app.dist.admin %>'
                    },
                    {
                        expand: true,
                        dot: true,
                        cwd: 'bower_components/angular-ui-grid',
                        src: ['*.eot', '*.svg', '*.ttf', '*.woff'],
                        dest: '<%= app.dist.admin %>/styles'
                    },
                    {
                        expand: true,
                        dot: true,
                        cwd: 'app/fonts/pe-icon-7-stroke/',
                        src: ['fonts/*.*'],
                        dest: '<%= app.dist.admin %>'
                    },
                ]
            },
            analytics: {
                files: [
                    {
                        expand: true,
                        dot: true,
                        cwd: '<%= app.appName %>',
                        dest: '<%= app.dist.analytics %>',
                        src: [
                            '*.{ico,png,txt}',
                            '.htaccess',
                            'analytics.html',
                            'views/common/{,*/}*.html',
                            'views/analytics/{,*/}*.html',
                            'styles/img/*.*',
                            'images/{,*/}*.*'
                        ]
                    },
                    {
                        expand: true,
                        dot: true,
                        cwd: 'bower_components/fontawesome',
                        src: ['fonts/*.*'],
                        dest: '<%= app.dist.analytics %>'
                    },
                    {
                        expand: true,
                        dot: true,
                        cwd: 'bower_components/bootstrap',
                        src: ['fonts/*.*'],
                        dest: '<%= app.dist.analytics %>'
                    },{
                        expand: true,
                        dot: true,
                        cwd: 'bower_components/angular-ui-grid',
                        src: ['*.eot', '*.svg', '*.ttf', '*.woff'],
                        dest: '<%= app.dist.analytics %>/styles'
                    },
                    {
                        expand: true,
                        dot: true,
                        cwd: 'app/fonts/pe-icon-7-stroke/',
                        src: ['fonts/*.*'],
                        dest: '<%= app.dist.analytics %>'
                    },
                ]
            },
            styles: {
                expand: true,
                cwd: '<%= app.appName %>/styles',
                dest: '.tmp/styles/',
                src: '{,*/}*.css'
            }
        },
        // Renames files for browser caching purposes
        filerev: {
            index: {
                src: [
                    '<%= app.dist.index %>/scripts/{,*/}*.js',
                    '<%= app.dist.index %>/styles/{,*/}*.css',
                    '<%= app.dist.index %>/styles/fonts/*'
                ]
            },
            admin: {
                src: [
                    '<%= app.dist.admin %>/scripts/{,*/}*.js',
                    '<%= app.dist.admin %>/styles/{,*/}*.css',
                    '<%= app.dist.admin %>/styles/fonts/*'
                ]
            },
            analytics: {
                src: [
                    '<%= app.dist.analytics %>/scripts/{,*/}*.js',
                    '<%= app.dist.analytics %>/styles/{,*/}*.css',
                    '<%= app.dist.analytics %>/styles/fonts/*'
                ]
            }
        },
        htmlmin: {
            index: {
                options: {
                    collapseWhitespace: true,
                    conservativeCollapse: true,
                    collapseBooleanAttributes: true,
                    removeCommentsFromCDATA: true,
                    removeOptionalTags: true
                },
                files: [{
                    expand: true,
                    cwd: '<%= app.dist.index %>',
                    src: ['*.html', 'views/{,*/}*.html'],
                    dest: '<%= app.dist.index %>'
                }]
            },
            admin: {
                options: {
                    collapseWhitespace: true,
                    conservativeCollapse: true,
                    collapseBooleanAttributes: true,
                    removeCommentsFromCDATA: true,
                    removeOptionalTags: true
                },
                files: [{
                    expand: true,
                    cwd: '<%= app.dist.admin %>',
                    src: ['*.html', 'views/{,*/}*.html'],
                    dest: '<%= app.dist.admin %>'
                }]
            },
            analytics: {
                options: {
                    collapseWhitespace: true,
                    conservativeCollapse: true,
                    collapseBooleanAttributes: true,
                    removeCommentsFromCDATA: true,
                    removeOptionalTags: true
                },
                files: [{
                    expand: true,
                    cwd: '<%= app.dist.analytics %>',
                    src: ['*.html', 'views/{,*/}*.html'],
                    dest: '<%= app.dist.analytics %>'
                }]
            }
        },
        useminPrepare: {
            index: {
                src:['app/index.html'],
                options: {
                            dest: '<%= app.dist.index %>'
                        }
            },
            admin: {
                src:['app/admin.html'],
                options: {
                    dest: '<%= app.dist.admin %>'
                }
            },
            analytics: {
                src:['app/analytics.html'],
                options: {
                    dest: '<%= app.dist.analytics %>'
                }
            }
        },
        usemin: {
            html: ['<%= app.dist.index %>/index.html', '<%= app.dist.admin %>/admin.html', '<%= app.dist.analytics %>/analytics.html'],
        },
        ngAnnotate:{
            options: {
                singleQuotes: true,
            }
        },
        html2js:  {
            index:{
                options: {
                    base: '<%= app.dist.index %>'
                },
                src: ['<%= app.dist.index %>/views/**/**.html'],
                dest: '<%= app.dist.index %>/scripts/templates.js',
                module: 'app.template'

            },
            admin:{
                options: {
                    base: '<%= app.dist.admin %>'
                },
                src: ['<%= app.dist.admin %>/views/**/**.html'],
                dest: '<%= app.dist.admin %>/scripts/templates.js',
                module: 'app.template'

            },
            analytics:{
                options: {
                    base: '<%= app.dist.analytics %>'
                },
                src: ['<%= app.dist.analytics %>/views/**/**.html'],
                dest: '<%= app.dist.analytics %>/scripts/templates.js',
                module: 'app.template'

            },

        }
    });

    grunt.registerTask('live', [
        'clean:server',
        'copy:styles',
        'connect:livereload',
        'watch'
    ]);

    grunt.registerTask('server', [
        'buildAdmin',
        'buildIndex',
        'buildAnalytics',
        'connect:dist:keepalive'
    ]);

    grunt.registerTask('buildIndex', [
        'clean:dist',
        'less',
        'useminPrepare:index',
        'concat',
        'ngAnnotate',
        'babel',
        'copy:index',
        'cssmin',
        'uglify',
        // 'filerev:index',
        'usemin:html:index',
        'htmlmin:index',
        'html2js:index'
    ]);

    grunt.registerTask('buildAdmin', [
        'clean:dist',
        'less',
        'useminPrepare:admin',
        'concat',
        'ngAnnotate',
        'babel',
        'copy:admin',
        'cssmin',
        'uglify',
        // 'filerev:index',
        'usemin:html:admin',
        'htmlmin:admin',
        'html2js:admin'
    ]);

    grunt.registerTask('buildAnalytics', [
        'clean:dist',
        'less',
        'useminPrepare:analytics',
        'concat',
        'ngAnnotate',
        'babel',
        'copy:analytics',
        'cssmin',
        'uglify',
        // 'filerev:index',
        'usemin:html:analytics  ',
        'htmlmin:analytics',
        'html2js:analytics'
    ]);

};
