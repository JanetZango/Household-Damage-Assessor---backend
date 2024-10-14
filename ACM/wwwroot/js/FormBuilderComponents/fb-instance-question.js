(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
        typeof define === 'function' && define.amd ? define(factory) :
            (global.VueFBInstanceQuestion = factory());
}(this, (function () {
    var index = {
        name: 'fb-instance-question',
        props: ['value', 'iindex', 'cindex', 'sindex', 'iiindex', 'categoryitems', 'readonly', 'diindex'],
        data: function () {
            return {
                
            };
        },
        computed: {
            formItemQuestionResponseID: function () {
                if (this.value != null && this.value.instanceProperties != null && this.value.instanceProperties.formItemQuestionResponseID != null) {
                    return this.value.instanceProperties.formItemQuestionResponseID
                }
                else {
                    return [];
                }
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
            enableitemfixing: function () {
                this.enableItemFixing = (this.enableitemfixing.toLowerCase() === "true");                
            },
            captureacknowledgment: function () {
                this.captureAcknowledgment = (this.captureacknowledgment.toLowerCase() === "true");
            },
            formItemQuestionResponseID: function () {
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
            validate: function () {
                if (this.isMandatory === true && this.visible == true) {
                    if (this.formItemQuestionResponseID != null && this.formItemQuestionResponseID.length > 0) {
                        this.value.isValid = true;
                    }
                    else {
                        this.value.isValid = false;
                    }
                }
                else {
                    this.value.isValid = true;
                }
            },
            isFaulureResultChosen: function () {
                var self = this;
                var returnValue = false;
                if (self.value != undefined && self.value != null && self.value.instanceProperties !== undefined && self.value.instanceProperties !== null && self.value.instanceProperties.formItemQuestionResponseID.length > 0) {
                    for (var i = 0; i < self.value.instanceProperties.formItemQuestionResponseID.length; i++) {
                        var answer = $.grep(self.value.definitionProperties.responseTypeAnswers, function (item) {
                            return item.formBuilderFormDefinitionStandardResponseTypeAnswerID.toLowerCase() === self.value.instanceProperties.formItemQuestionResponseID[i].toLowerCase();
                        });

                        if (answer !== null && answer.length > 0 && answer[0].isFailure === true) {
                            returnValue = true;
                        }
                    }
                }

                this.value.negativeResult = returnValue;

                return returnValue;
            },
            selectedOptions: function () {
                var self = this;
                var returnValue = "";
                if (self.value != undefined && self.value != null && self.value.instanceProperties !== undefined && self.value.instanceProperties !== null && self.value.instanceProperties.formItemQuestionResponseID.length > 0) {
                    for (var i = 0; i < self.value.instanceProperties.formItemQuestionResponseID.length; i++) {
                        var answer = $.grep(self.value.definitionProperties.responseTypeAnswers, function (item) {
                            return item.formBuilderFormDefinitionStandardResponseTypeAnswerID.toLowerCase() === self.value.instanceProperties.formItemQuestionResponseID[i].toLowerCase();
                        });

                        if (i > 0) {
                            returnValue += ", ";
                        }
                        if (answer !== null && answer.length > 0) {
                            returnValue += answer[0].description;
                        }
                    }
                }
                return returnValue;
            },
            SetAnswer: function (formBuilderFormDefinitionStandardResponseTypeAnswerID) {

                var itemExists = $.grep(this.value.instanceProperties.formItemQuestionResponseID, function (item) {
                    return item.toLowerCase() == formBuilderFormDefinitionStandardResponseTypeAnswerID.toLowerCase();
                });

                if (itemExists == null || itemExists.length == 0) {
                    this.value.instanceProperties.formItemQuestionResponseID = [];
                    this.value.instanceProperties.formItemQuestionResponseID.push(formBuilderFormDefinitionStandardResponseTypeAnswerID);
                }
                else {
                    this.value.instanceProperties.formItemQuestionResponseID = [];
                }

                this.update('instanceProperties.formItemQuestionResponseID', this.value.instanceProperties.formItemQuestionResponseID);
            },
            SetAnswerMultiple: function (event, formBuilderFormDefinitionStandardResponseTypeAnswerID) {

                if (event.target.checked === true) {
                    var itemExixt = $.grep(this.value.instanceProperties.formItemQuestionResponseID, function (item) {
                        return item.toLowerCase() == formBuilderFormDefinitionStandardResponseTypeAnswerID.toLowerCase();
                    });

                    if (itemExixt == null || itemExixt.length == 0) {
                        this.value.instanceProperties.formItemQuestionResponseID.push(formBuilderFormDefinitionStandardResponseTypeAnswerID);
                    }
                }
                else {
                    this.value.instanceProperties.formItemQuestionResponseID = $.grep(this.value.instanceProperties.formItemQuestionResponseID, function (item) {
                        return item.toLowerCase() !== formBuilderFormDefinitionStandardResponseTypeAnswerID.toLowerCase();
                    });
                }

                this.update('instanceProperties.formItemQuestionResponseID', this.value.instanceProperties.formItemQuestionResponseID);
            }
        },
        template: `<div class="question fb-instance-item" v-if="value.childCode == 'QUESTION' && visible == true">
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
                                                    <div class="row non-list-response" v-if="value.definitionProperties != undefined && value.definitionProperties.responseTypeAnswers != undefined && value.definitionProperties.responseTypeAnswers.length > 0 && value.definitionProperties.isList == false">
                                                        <div class="col">
                                                            <button type="button" v-for="answer in value.definitionProperties.responseTypeAnswers" :disabled="readonly" :data-answer-id="answer.formBuilderFormDefinitionStandardResponseTypeAnswerID" 
                                                                v-on:click="SetAnswer(answer.formBuilderFormDefinitionStandardResponseTypeAnswerID)" 
                                                                :class="['btn', 'btn-block', {'btn-secondary': value.instanceProperties.formItemQuestionResponseID.length == 0 || value.instanceProperties.formItemQuestionResponseID[0].toLowerCase() != answer.formBuilderFormDefinitionStandardResponseTypeAnswerID.toLowerCase()}, {'btn-success': value.instanceProperties.formItemQuestionResponseID.length > 0 && answer.isFailure == false && isFaulureResultChosen() == false && value.instanceProperties.formItemQuestionResponseID[0].toLowerCase() == answer.formBuilderFormDefinitionStandardResponseTypeAnswerID.toLowerCase()}, {'btn-danger': value.instanceProperties.formItemQuestionResponseID.length > 0 && answer.isFailure == true && isFaulureResultChosen() == true && value.instanceProperties.formItemQuestionResponseID[0].toLowerCase() == answer.formBuilderFormDefinitionStandardResponseTypeAnswerID.toLowerCase()}]">{{answer.description}}</button>
                                                        </div>
                                                    </div>
                                                    <div class="row list-response" v-if="value.definitionProperties != undefined && value.definitionProperties.responseTypeAnswers != undefined && value.definitionProperties.responseTypeAnswers.length > 0 && value.definitionProperties.isList == true">
                                                        <div class="col-xs-12">
                                                            <button type="button" :disabled="readonly" class="btn btn-block btn-secondary" data-toggle="modal" :data-target="'#basic_answer_modal_' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex">
                                                                Select response
                                                            </button>
                                                            <div class="modal" :id="'basic_answer_modal_' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" tabindex="-1" role="dialog">
                                                                <div class="modal-dialog modal-sm" role="document">
                                                                    <div class="modal-content" style="border-radius: 5px;">
                                                                        <div class="modal-body">
                                                                            <div v-if="value.definitionProperties.isListMultiSelect == false">
                                                                                <div class="btn-group-vertical full-width" role="group">
                                                                                    <button type="button" v-for="answer in value.definitionProperties.responseTypeAnswers" v-on:click="SetAnswer(answer.formBuilderFormDefinitionStandardResponseTypeAnswerID)"  data-dismiss="modal" :data-answer-id="answer.formBuilderFormDefinitionStandardResponseTypeAnswerID" class="btn btn-block btn-secondary">{{answer.description}}</button>
                                                                                </div>
                                                                            </div>
                                                                            <div v-if="value.definitionProperties.isListMultiSelect == true">
                                                                                <template v-for="answer in value.definitionProperties.responseTypeAnswers">
                                                                                    <label class="anglo-checkbox">
                                                                                        {{answer.description}}
                                                                                        <input type="checkbox" v-on:click="SetAnswerMultiple($event, answer.formBuilderFormDefinitionStandardResponseTypeAnswerID)" :data-answer-id="answer.formBuilderFormDefinitionStandardResponseTypeAnswerID">
                                                                                        <span class="checkmark"></span>
                                                                                    </label>
                                                                                    <hr style="margin:0; padding:0" />
                                                                                </template>
                                                                            </div>
                                                                        </div>
                                                                        <div class="modal-footer" v-if="value.definitionProperties.isListMultiSelect == true">
                                                                            <button type="button" class="btn btn-primary btn-block" data-dismiss="modal">Done</button>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div><br />
                                                    <div class="row" v-if="selectedOptions() != null && selectedOptions() != '' && value.definitionProperties.isList == true">
                                                        <div class="col-md-12">
                                                           <i v-if="isFaulureResultChosen() == false" class="anglo-blue fa fa-check"></i>
                                                            <i v-if="isFaulureResultChosen() == true" class="anglo-red fa fa-exclamation-triangle"></i> {{selectedOptions()}}
                                                        </div>
                                                    </div>
                                                    <div class="row" v-if="(selectedOptions() == null || selectedOptions() == '') && readonly == true">
                                                        <div class="col-md-12">
                                                           No answer selected
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
    };
    return index;
})));