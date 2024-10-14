(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
        typeof define === 'function' && define.amd ? define(factory) :
            (global.VueFBInstanceNumber = factory());
}(this, (function () {
    var index = {
        name: 'fb-instance-number',
        props: ['value', 'iindex', 'cindex', 'sindex', 'iiindex', 'categoryitems', 'readonly', 'diindex'],
        data: function () {
            return {
                
            };
        },
        computed: {
            numberValue: function () {
                return this.value.instanceProperties.numberValue;
            },
            isMandatory: function () {
                var returnValue = false;
                var self = this;

                if (self.value.definitionProperties.isMandatory == true) {
                    returnValue = true;
                }
                else if (self.value != undefined && self.value.definitionProperties != undefined && self.value != null && self.value.definitionProperties != null
                    && self.categoryitems != undefined
                    && self.value.definitionProperties.isCondition == true && self.value.definitionProperties.conditionActionEventCode == 'REQUIRED') {

                    var parentQuestion = $.grep(self.categoryitems, function (item) {
                        var returnValue = false;

                        if (item.childCode == 'QUESTION') {
                            if (item.definitionChildID.toLowerCase() == self.value.definitionProperties.conditionFormDefinitionItemQuestionID.toLowerCase()) {
                                return true;
                            }
                        }

                        return returnValue;
                    });

                    if (parentQuestion != undefined && parentQuestion != null && parentQuestion.length > 0) {
                        var selectedAnswer = $.grep(parentQuestion[0].instanceProperties.formItemQuestionResponseID, function (answer) {
                            return answer.toLowerCase() == self.value.definitionProperties.conditionFormDefinitionItemQuestionResponseID.toLowerCase();
                        });

                        if (selectedAnswer != null && selectedAnswer.length > 0) {
                            returnValue = true;
                        }
                    }
                }

                return returnValue;
            },
            visible: function () {
                var returnValue = false;
                var self = this;

                if (self.value != undefined && self.value.definitionProperties != undefined && self.value != null && self.value.definitionProperties != null
                    && self.categoryitems != undefined && self.value.definitionProperties.isCondition == true) {

                    var selectedAnswer;
                    var parentQuestion = $.grep(self.categoryitems, function (item) {
                        var returnValue = false;

                        if (item.childCode == 'QUESTION') {
                            if (item.definitionChildID.toLowerCase() == self.value.definitionProperties.conditionFormDefinitionItemQuestionID.toLowerCase()) {
                                return true;
                            }
                        }

                        return returnValue;
                    });

                    if (self.value.definitionProperties.conditionActionEventCode == 'VISIBLE') {

                        if (parentQuestion != undefined && parentQuestion != null && parentQuestion.length > 0) {
                            selectedAnswer = $.grep(parentQuestion[0].instanceProperties.formItemQuestionResponseID, function (answer) {
                                return answer.toLowerCase() == self.value.definitionProperties.conditionFormDefinitionItemQuestionResponseID.toLowerCase();
                            });

                            if (selectedAnswer != null && selectedAnswer.length > 0) {
                                returnValue = true;
                            }
                            else {
                                returnValue = false;
                            }
                        }
                    }
                    else if (self.value.definitionProperties.conditionActionEventCode == 'HIDDEN') {

                        if (parentQuestion != undefined && parentQuestion != null && parentQuestion.length > 0) {
                            selectedAnswer = $.grep(parentQuestion[0].instanceProperties.formItemQuestionResponseID, function (answer) {
                                return answer.toLowerCase() == self.value.definitionProperties.conditionFormDefinitionItemQuestionResponseID.toLowerCase();
                            });

                            if (selectedAnswer != null && selectedAnswer.length > 0) {
                                returnValue = false;
                            }
                            else {
                                returnValue = true;
                            }
                        }
                    }
                    else {
                        returnValue = true;
                    }
                }
                else {
                    returnValue = true;
                }

                return returnValue;
            }
        },
        watch: {
            numberValue: function () {
                this.validate();
            }
        },
        created: function () {
            
        },
        mounted: function () {
            var self = this;
            setTimeout(function () { self.validate(); }, 1000);
        },
        methods: {
            update(key, value) {
                this.$emit('input', _.tap(_.cloneDeep(this.value), v => _.set(v, key, value)));
            },
            isFaulureResultChosen: function () {
                var returnValue = false;

                if (this.numberValue != null && this.numberValue != '') {
                    if (this.value.definitionProperties.tolleranceComparisonOperator == '<' && (this.value.instanceProperties.numberValue > this.value.definitionProperties.toleranceRangeFrom)) {
                        returnValue = true;
                    }
                    else if (this.value.definitionProperties.tolleranceComparisonOperator == '>' && (this.value.instanceProperties.numberValue < this.value.definitionProperties.toleranceRangeFrom)) {
                        returnValue = true;
                    }
                    else if (this.value.definitionProperties.tolleranceComparisonOperator == '=' && (this.value.instanceProperties.numberValue != this.value.definitionProperties.toleranceRangeFrom)) {
                        returnValue = true;
                    }
                    else if (this.value.definitionProperties.tolleranceComparisonOperator == '<=>' && (this.value.instanceProperties.numberValue < this.value.definitionProperties.toleranceRangeFrom || this.value.instanceProperties.numberValue > this.value.definitionProperties.toleranceRangeTo)) {
                        returnValue = true;
                    }
                }

                return returnValue;
            },
            validate: function () {
                if (this.isMandatory === true && this.visible == true) {
                    if (this.numberValue != null && this.numberValue != '') {
                        if (this.value.definitionProperties.isSystemMeasurement == true && this.value.instanceProperties.isError == true) {
                            this.value.isValid = false;
                        }
                        else if (this.value.definitionProperties.isSystemMeasurement == true && this.value.instanceProperties.isSubmitted == false) {
                            this.value.isValid = false;
                        }
                        else {
                            this.value.isValid = true;
                        }
                    }
                    else {
                        this.value.isValid = false;
                    }
                }
                else {
                    this.value.isValid = true;
                }
            },
        },
        template: `<div class="number fb-instance-item" v-if="value.childCode == 'NUMBER' && visible == true">
                        <div class="row">
                              <div class="col-md-12">
                                   <strong class="item-description">{{value.childDescription}}</strong> <span style="color:red;" v-if="isMandatory == true">*</span>
                              </div>
                        </div>
                        <div class="row">
                              <div class="col-md-12">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" v-bind:href="'#fbitemresult' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" v-bind:id="'fbitemresult-tab' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" role="tab" data-toggle="tab" v-bind:aria-controls="'fbitemresult' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" aria-expanded="true">Result</a></li>
                                        <li class="nav-item">
                                            <a class="nav-link" v-bind:href="'#fbitemfeedback' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" role="tab" v-bind:id="'fbitemfeedback-tab' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" data-toggle="tab" v-bind:aria-controls="'fbitemfeedback' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex"><i v-if="(value.instanceItem.remarks != null && value.instanceItem.remarks.length > 0) || (value.instanceItem.files != null && value.instanceItem.files.length > 0)" class="anglo-blue fa fa-check"></i> Feedback</a>
                                        </li>
                                    </ul>
                                    <div class="tab-content px-1 pt-1 border-left border-bottom border-right">
                                        <div role="tabpanel" class="tab-pane active p-1" v-bind:id="'fbitemresult' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" v-bind:aria-labelledby="'fbitemresult-tab' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex">
                                            <div class="row">
                                                <div class="col-md-6">
                                                     <div class="row">
                                                        <div class="col-xs-6" v-if="value.definitionProperties.isSystemMeasurement == false">
                                                            <input type="number" :disabled="readonly" class="form-control" v-model="value.instanceProperties.numberValue" @input="update('instanceProperties.numberValue', $event.target.value)" />
                                                        </div>
                                                        <div class="col-xs-6" v-if="value.definitionProperties.isSystemMeasurement == true">
                                                            <div class="input-group mar-btm">
					                                            <input type="text" :disabled="readonly || value.instanceProperties.isSubmitted == true" class="form-control" v-model="value.instanceProperties.numberValue" @input="update('instanceProperties.numberValue', $event.target.value)" />
					                                        </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <label class="form-control-static">{{value.definitionProperties.unitOfMeasure}}</label>
                                                        </div>
                                                    </div>
                                                    <div class="row mar-btm" v-if="value.definitionProperties.isTolerance == true && value.definitionProperties.toleranceRangeFriendlyMessage != null && value.definitionProperties.toleranceRangeFriendlyMessage != ''">
                                                        <div class="col-xs-12">
                                                            <span>{{value.definitionProperties.toleranceRangeFriendlyMessage}}</span>
                                                        </div>
                                                    </div>
                                                    <div class="row mar-btm" v-if="value.definitionProperties.isTolerance == true">
                                                        <div class="col-xs-12">
                                                            <div class="alert alert-danger" v-if="isFaulureResultChosen() == true && value.definitionProperties.toleranceExceededPrompt != null && value.definitionProperties.toleranceExceededPrompt != ''">
                                                                <i class="fa fa-exclamation-triangle"></i> <span>{{value.definitionProperties.toleranceExceededPrompt}}</span>
                                                            </div>
                                                            <div class="alert alert-danger" v-if="isFaulureResultChosen() == true && (value.definitionProperties.toleranceExceededPrompt == null || value.definitionProperties.toleranceExceededPrompt == '')">
                                                                <i class="fa fa-exclamation-triangle"></i> <span>Value entered is outside of the tolerance range</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row mar-btm" v-if="value.definitionProperties.displayPreviousMeasurement">
                                                        <div class="col-xs-12 text-muted">
                                                            <span v-if="value.instanceProperties.lastMeterReading == 0">Previous reading: no previous reading available</span>
                                                            <span v-if="value.instanceProperties.lastMeterReading > 0">Previous reading: {{value.instanceProperties.lastMeterReading}}, captured on {{value.instanceProperties.lastMeterReadingDate}} by {{value.instanceProperties.lastMeterReadingOperator}}</span>
                                                        </div>
                                                    </div>
                                                    <div class="row" v-if="value.definitionProperties.isSystemMeasurement == true && value.instanceProperties.isError == true">
                                                        <div class="col-xs-12">
                                                            <div class="text-danger">
                                                                <i class="fa fa-exclamation-triangle"></i> <span>{{value.instanceProperties.errorMessage}}</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <fb-instance-caution-instruction v-model="value"></fb-instance-caution-instruction>
                                                </div>
                                            </div>
                                        </div>
                                        <div role="tabpanel" class="tab-pane" v-bind:id="'fbitemfeedback' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" v-bind:aria-labelledby="'fbitemfeedback-tab' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex">
                                              <fb-instance-feedback v-model="value.instanceItem" :readonly="readonly" :isfaulureresultchosen="isFaulureResultChosen()"></fb-instance-feedback>
                                        </div>
                                    </div>
                              </div>
                        </div>
                    </div>`
    }
    return index;
})));