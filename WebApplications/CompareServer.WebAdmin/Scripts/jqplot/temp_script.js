


$.jqplot.LinearAxisRenderer.prototype.createTicks = function (plot) {
    // we're are operating on an axis here
    var ticks = this._ticks;
    var userTicks = this.ticks;
    var name = this.name;
    // databounds were set on axis initialization.
    var db = this._dataBounds;
    var dim = (this.name.charAt(0) === 'x') ? this._plotDimensions.width : this._plotDimensions.height;
    var interval;
    var min, max;
    var pos1, pos2;
    var tt, i;
    // get a copy of user's settings for min/max.
    var userMin = this.min;
    var userMax = this.max;
    var userNT = this.numberTicks;
    var userTI = this.tickInterval;

    var threshold = 30;
    this._scalefact = (Math.max(dim, threshold + 1) - threshold) / 300.0;

    // if we already have ticks, use them.
    // ticks must be in order of increasing value.

    if (userTicks.length) {
        // ticks could be 1D or 2D array of [val, val, ,,,] or [[val, label], [val, label], ...] or mixed
        for (i = 0; i < userTicks.length; i++) {
            var ut = userTicks[i];
            var t = new this.tickRenderer(this.tickOptions);
            if ($.isArray(ut)) {
                t.value = ut[0];
                if (this.breakPoints) {
                    if (ut[0] == this.breakPoints[0]) {
                        t.label = this.breakTickLabel;
                        t._breakTick = true;
                        t.showGridline = false;
                        t.showMark = false;
                    }
                    else if (ut[0] > this.breakPoints[0] && ut[0] <= this.breakPoints[1]) {
                        t.show = false;
                        t.showGridline = false;
                        t.label = ut[1];
                    }
                    else {
                        t.label = ut[1];
                    }
                }
                else {
                    t.label = ut[1];
                }
                t.setTick(ut[0], this.name);
                this._ticks.push(t);
            }

            else if ($.isPlainObject(ut)) {
                $.extend(true, t, ut);
                t.axis = this.name;
                this._ticks.push(t);
            }

            else {
                t.value = ut;
                if (this.breakPoints) {
                    if (ut == this.breakPoints[0]) {
                        t.label = this.breakTickLabel;
                        t._breakTick = true;
                        t.showGridline = false;
                        t.showMark = false;
                    }
                    else if (ut > this.breakPoints[0] && ut <= this.breakPoints[1]) {
                        t.show = false;
                        t.showGridline = false;
                    }
                }
                t.setTick(ut, this.name);
                this._ticks.push(t);
            }
        }
        this.numberTicks = userTicks.length;
        this.min = this._ticks[0].value;
        this.max = this._ticks[this.numberTicks - 1].value;
        this.tickInterval = (this.max - this.min) / (this.numberTicks - 1);
    }

    // we don't have any ticks yet, let's make some!
    else {
        if (name == 'xaxis' || name == 'x2axis') {
            dim = this._plotDimensions.width;
        }
        else {
            dim = this._plotDimensions.height;
        }

        var _numberTicks = this.numberTicks;

        // if aligning this axis, use number of ticks from previous axis.
        // Do I need to reset somehow if alignTicks is changed and then graph is replotted??
        if (this.alignTicks) {
            if (this.name === 'x2axis' && plot.axes.xaxis.show) {
                _numberTicks = plot.axes.xaxis.numberTicks;
            }
            else if (this.name.charAt(0) === 'y' && this.name !== 'yaxis' && this.name !== 'yMidAxis' && plot.axes.yaxis.show) {
                _numberTicks = plot.axes.yaxis.numberTicks;
            }
        }

        min = ((this.min != null) ? this.min : db.min);
        max = ((this.max != null) ? this.max : db.max);

        var range = max - min;
        var rmin, rmax;
        var temp;

        if (this.tickOptions == null || !this.tickOptions.formatString) {
            this._overrideFormatString = true;
        }

        // Doing complete autoscaling
        if (this.min == null && this.max == null && this.tickInterval == null && !this.autoscale) {
            // Check if user must have tick at 0 or 100 and ensure they are in range.
            // The autoscaling algorithm will always place ticks at 0 and 100 if they are in range.
            if (this.forceTickAt0) {
                if (min > 0) {
                    min = 0;
                }
                if (max < 0) {
                    max = 0;
                }
            }

            if (this.forceTickAt100) {
                if (min > 100) {
                    min = 100;
                }
                if (max < 100) {
                    max = 100;
                }
            }

            // var threshold = 30;
            // var tdim = Math.max(dim, threshold+1);
            // this._scalefact =  (tdim-threshold)/300.0;
            var ret = $.jqplot.LinearTickGenerator(min, max, this._scalefact, _numberTicks);
            // calculate a padded max and min, points should be less than these
            // so that they aren't too close to the edges of the plot.
            // User can adjust how much padding is allowed with pad, padMin and PadMax options. 
            var tumin = min + range * (this.padMin - 1);
            var tumax = max - range * (this.padMax - 1);

            // if they're equal, we shouldn't have to do anything, right?
            // if (min <=tumin || max >= tumax) {
            if (min < tumin || max > tumax) {
                tumin = min - range * (this.padMin - 1);
                tumax = max + range * (this.padMax - 1);
                ret = $.jqplot.LinearTickGenerator(tumin, tumax, this._scalefact, _numberTicks);
            }

            this.min = ret[0];
            this.max = ret[1];
            // if numberTicks specified, it should return the same.
            this.numberTicks = ret[2];
            this._autoFormatString = ret[3];
            this.tickInterval = ret[4];
        }

        // User has specified some axis scale related option, can use auto algorithm
        else {

            // if min and max are same, space them out a bit
            if (min == max) {
                var adj = 0.05;
                if (min > 0) {
                    adj = Math.max(Math.log(min) / Math.LN10, 0.05);
                }
                min -= adj;
                max += adj;
            }

            // autoscale.  Can't autoscale if min or max is supplied.
            // Will use numberTicks and tickInterval if supplied.  Ticks
            // across multiple axes may not line up depending on how
            // bars are to be plotted.
            if (this.autoscale && this.min == null && this.max == null) {
                var rrange, ti, margin;
                var forceMinZero = false;
                var forceZeroLine = false;
                var intervals = { min: null, max: null, average: null, stddev: null };
                // if any series are bars, or if any are fill to zero, and if this
                // is the axis to fill toward, check to see if we can start axis at zero.
                for (var i = 0; i < this._series.length; i++) {
                    var s = this._series[i];
                    var faname = (s.fillAxis == 'x') ? s._xaxis.name : s._yaxis.name;
                    // check to see if this is the fill axis
                    if (this.name == faname) {
                        var vals = s._plotValues[s.fillAxis];
                        var vmin = vals[0];
                        var vmax = vals[0];
                        for (var j = 1; j < vals.length; j++) {
                            if (vals[j] < vmin) {
                                vmin = vals[j];
                            }
                            else if (vals[j] > vmax) {
                                vmax = vals[j];
                            }
                        }
                        var dp = (vmax - vmin) / vmax;
                        // is this sries a bar?
                        if (s.renderer.constructor == $.jqplot.BarRenderer) {
                            // if no negative values and could also check range.
                            if (vmin >= 0 && (s.fillToZero || dp > 0.1)) {
                                forceMinZero = true;
                            }
                            else {
                                forceMinZero = false;
                                if (s.fill && s.fillToZero && vmin < 0 && vmax > 0) {
                                    forceZeroLine = true;
                                }
                                else {
                                    forceZeroLine = false;
                                }
                            }
                        }

                        // if not a bar and filling, use appropriate method.
                        else if (s.fill) {
                            if (vmin >= 0 && (s.fillToZero || dp > 0.1)) {
                                forceMinZero = true;
                            }
                            else if (vmin < 0 && vmax > 0 && s.fillToZero) {
                                forceMinZero = false;
                                forceZeroLine = true;
                            }
                            else {
                                forceMinZero = false;
                                forceZeroLine = false;
                            }
                        }

                        // if not a bar and not filling, only change existing state
                        // if it doesn't make sense
                        else if (vmin < 0) {
                            forceMinZero = false;
                        }
                    }
                }

                // check if we need make axis min at 0.
                if (forceMinZero) {
                    // compute number of ticks
                    this.numberTicks = 2 + Math.ceil((dim - (this.tickSpacing - 1)) / this.tickSpacing);
                    this.min = 0;
                    userMin = 0;
                    // what order is this range?
                    // what tick interval does that give us?
                    ti = max / (this.numberTicks - 1);
                    temp = Math.pow(10, Math.abs(Math.floor(Math.log(ti) / Math.LN10)));
                    if (ti / temp == parseInt(ti / temp, 10)) {
                        ti += temp;
                    }
                    this.tickInterval = Math.ceil(ti / temp) * temp;
                    this.max = this.tickInterval * (this.numberTicks - 1);
                }

                // check if we need to make sure there is a tick at 0.
                else if (forceZeroLine) {
                    // compute number of ticks
                    this.numberTicks = 2 + Math.ceil((dim - (this.tickSpacing - 1)) / this.tickSpacing);
                    var ntmin = Math.ceil(Math.abs(min) / range * (this.numberTicks - 1));
                    var ntmax = this.numberTicks - 1 - ntmin;
                    ti = Math.max(Math.abs(min / ntmin), Math.abs(max / ntmax));
                    temp = Math.pow(10, Math.abs(Math.floor(Math.log(ti) / Math.LN10)));
                    this.tickInterval = Math.ceil(ti / temp) * temp;
                    this.max = this.tickInterval * ntmax;
                    this.min = -this.tickInterval * ntmin;
                }

                // if nothing else, do autoscaling which will try to line up ticks across axes.
                else {
                    if (this.numberTicks == null) {
                        if (this.tickInterval) {
                            this.numberTicks = 3 + Math.ceil(range / this.tickInterval);
                        }
                        else {
                            this.numberTicks = 2 + Math.ceil((dim - (this.tickSpacing - 1)) / this.tickSpacing);
                        }
                    }

                    if (this.tickInterval == null) {
                        // get a tick interval
                        ti = range / (this.numberTicks - 1);

                        if (ti < 1) {
                            temp = Math.pow(10, Math.abs(Math.floor(Math.log(ti) / Math.LN10)));
                        }
                        else {
                            temp = 1;
                        }
                        this.tickInterval = Math.ceil(ti * temp * this.pad) / temp;
                    }
                    else {
                        temp = 1 / this.tickInterval;
                    }

                    // try to compute a nicer, more even tick interval
                    // temp = Math.pow(10, Math.floor(Math.log(ti)/Math.LN10));
                    // this.tickInterval = Math.ceil(ti/temp) * temp;
                    rrange = this.tickInterval * (this.numberTicks - 1);
                    margin = (rrange - range) / 2;

                    if (this.min == null) {
                        this.min = Math.floor(temp * (min - margin)) / temp;
                    }
                    if (this.max == null) {
                        this.max = this.min + rrange;
                    }
                }

                // Compute a somewhat decent format string if it is needed.
                // get precision of interval and determine a format string.
                var sf = $.jqplot.getSignificantFigures(this.tickInterval);

                var fstr;

                // if we have only a whole number, use integer formatting
                if (sf.digitsLeft >= sf.significantDigits) {
                    fstr = '%d';
                }

                else {
                    var temp = Math.max(0, 5 - sf.digitsLeft);
                    temp = Math.min(temp, sf.digitsRight);
                    fstr = '%.' + temp + 'f';
                }

                this._autoFormatString = fstr;
            }

            // Use the default algorithm which pads each axis to make the chart
            // centered nicely on the grid.
            else {

                rmin = (this.min != null) ? this.min : min - range * (this.padMin - 1);
                rmax = (this.max != null) ? this.max : max + range * (this.padMax - 1);
                range = rmax - rmin;

                if (this.numberTicks == null) {
                    // if tickInterval is specified by user, we will ignore computed maximum.
                    // max will be equal or greater to fit even # of ticks.
                    if (this.tickInterval != null) {
                        this.numberTicks = Math.ceil((rmax - rmin) / this.tickInterval) + 1;
                    }
                    else if (dim > 100) {
                        this.numberTicks = parseInt(3 + (dim - 100) / 75, 10);
                    }
                    else {
                        this.numberTicks = 2;
                    }
                }

                if (this.tickInterval == null) {
                    this.tickInterval = range / (this.numberTicks - 1);
                }

                if (this.max == null) {
                    rmax = rmin + this.tickInterval * (this.numberTicks - 1);
                }
                if (this.min == null) {
                    rmin = rmax - this.tickInterval * (this.numberTicks - 1);
                }

                // get precision of interval and determine a format string.
                var sf = $.jqplot.getSignificantFigures(this.tickInterval);

                var fstr;

                // if we have only a whole number, use integer formatting
                if (sf.digitsLeft >= sf.significantDigits) {
                    fstr = '%d';
                }

                else {
                    var temp = Math.max(0, 5 - sf.digitsLeft);
                    temp = Math.min(temp, sf.digitsRight);
                    fstr = '%.' + temp + 'f';
                }


                this._autoFormatString = fstr;

                this.min = rmin;
                this.max = rmax;
            }

            if (this.renderer.constructor == $.jqplot.LinearAxisRenderer && this._autoFormatString == '') {
                // fix for misleading tick display with small range and low precision.
                range = this.max - this.min;
                // figure out precision
                var temptick = new this.tickRenderer(this.tickOptions);
                // use the tick formatString or, the default.
                var fs = temptick.formatString || $.jqplot.config.defaultTickFormatString;
                var fs = fs.match($.jqplot.sprintf.regex)[0];
                var precision = 0;
                if (fs) {
                    if (fs.search(/[fFeEgGpP]/) > -1) {
                        var m = fs.match(/\%\.(\d{0,})?[eEfFgGpP]/);
                        if (m) {
                            precision = parseInt(m[1], 10);
                        }
                        else {
                            precision = 6;
                        }
                    }
                    else if (fs.search(/[di]/) > -1) {
                        precision = 0;
                    }
                    // fact will be <= 1;
                    var fact = Math.pow(10, -precision);
                    if (this.tickInterval < fact) {
                        // need to correct underrange
                        if (userNT == null && userTI == null) {
                            this.tickInterval = fact;
                            if (userMax == null && userMin == null) {
                                // this.min = Math.floor((this._dataBounds.min - this.tickInterval)/fact) * fact;
                                this.min = Math.floor(this._dataBounds.min / fact) * fact;
                                if (this.min == this._dataBounds.min) {
                                    this.min = this._dataBounds.min - this.tickInterval;
                                }
                                // this.max = Math.ceil((this._dataBounds.max + this.tickInterval)/fact) * fact;
                                this.max = Math.ceil(this._dataBounds.max / fact) * fact;
                                if (this.max == this._dataBounds.max) {
                                    this.max = this._dataBounds.max + this.tickInterval;
                                }
                                var n = (this.max - this.min) / this.tickInterval;
                                n = n.toFixed(11);
                                n = Math.ceil(n);
                                this.numberTicks = n + 1;
                            }
                            else if (userMax == null) {
                                // add one tick for top of range.
                                var n = (this._dataBounds.max - this.min) / this.tickInterval;
                                n = n.toFixed(11);
                                this.numberTicks = Math.ceil(n) + 2;
                                this.max = this.min + this.tickInterval * (this.numberTicks - 1);
                            }
                            else if (userMin == null) {
                                // add one tick for bottom of range.
                                var n = (this.max - this._dataBounds.min) / this.tickInterval;
                                n = n.toFixed(11);
                                this.numberTicks = Math.ceil(n) + 2;
                                this.min = this.max - this.tickInterval * (this.numberTicks - 1);
                            }
                            else {
                                // calculate a number of ticks so max is within axis scale
                                this.numberTicks = Math.ceil((userMax - userMin) / this.tickInterval) + 1;
                                // if user's min and max don't fit evenly in ticks, adjust.
                                // This takes care of cases such as user min set to 0, max set to 3.5 but tick
                                // format string set to %d (integer ticks)
                                this.min = Math.floor(userMin * Math.pow(10, precision)) / Math.pow(10, precision);
                                this.max = Math.ceil(userMax * Math.pow(10, precision)) / Math.pow(10, precision);
                                // this.max = this.min + this.tickInterval*(this.numberTicks-1);
                                this.numberTicks = Math.ceil((this.max - this.min) / this.tickInterval) + 1;
                            }
                        }
                    }
                }
            }

        }

        if (this._overrideFormatString && this._autoFormatString != '') {
            this.tickOptions = this.tickOptions || {};
            this.tickOptions.formatString = this._autoFormatString;
        }

        var t, to;
        for (var i = 0; i < this.numberTicks; i++) {
            tt = this.min + i * this.tickInterval;
            t = new this.tickRenderer(this.tickOptions);
            // var t = new $.jqplot.AxisTickRenderer(this.tickOptions);

            t.setTick(tt, this.name);
            this._ticks.push(t);

            if (i < this.numberTicks - 1) {
                for (var j = 0; j < this.minorTicks; j++) {
                    tt += this.tickInterval / (this.minorTicks + 1);
                    to = $.extend(true, {}, this.tickOptions, { name: this.name, value: tt, label: '', isMinorTick: true });
                    t = new this.tickRenderer(to);
                    this._ticks.push(t);
                }
            }
            t = null;
        }
    }

    if (this.tickInset) {
        this.min = this.min - this.tickInset * this.tickInterval;
        this.max = this.max + this.tickInset * this.tickInterval;
    }

    ticks = null;
};
